using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ARINC_424_Encoder;

namespace EncoderTester
{
    public class Airport
    {
        public string Ident { get; }
        public Dictionary<string, Waypoint> Waypoints { get; }
        public Dictionary<string, Procedure> Approaches { get; }

        public Airport(string ident)
        {
            Ident = ident;
            Waypoints = new Dictionary<string, Waypoint>();
            Approaches = new Dictionary<string, Procedure>();
        }
    }

    public class Waypoint
    {
        public string Ident { get; }

        public Waypoint(string ident)
        {
            Ident = ident;
        }
    }

    public class Procedure
    {
        public string Ident { get; }
        public Dictionary<string, Transition> Transitions { get; }

        public Procedure(string ident)
        {
            Ident = ident;
            Transitions = new Dictionary<string, Transition>();
        }
    }

    public class Transition
    {
        public string Ident { get; }

        public Transition(string ident)
        {
            Ident = ident;
        }
    }
    
    internal class Program
    {
        public static void Main(string[] args)
        {
            var airports = new Dictionary<string, Airport>();

            var pathToFile = "C:/Users/evgeny.vasilyev/Downloads/CIFP_210128/FAACIFP18";
            using (var reader = new ArincFileReader(pathToFile, ArincSpecification.ARINC424_17))
            {
                IRecord record;
                while ((record = reader.ReadRecord()) != null)
                {
                    switch (record)
                    {
                        case AirportReferencePointRecord referencePointRecord:
                        {
                            var airport = new Airport(referencePointRecord.AirportIdent);
                            airports.Add(airport.Ident, airport);
                            break;
                        }
                        case AirportTerminalWaypointRecord terminalWaypointRecord:
                        {
                            var airport = airports[terminalWaypointRecord.AirportIdent];
                            var waypoint = new Waypoint(terminalWaypointRecord.WaypointIdent);
                            airport.Waypoints.Add(waypoint.Ident, waypoint);
                            break;
                        }
                        case AirportApproachProcedureRecord approachProcedureRecord:
                        {
                            var airport = airports[approachProcedureRecord.AirportIdent];
                            var procedureIdent = approachProcedureRecord.ProcedureIdent;
                            Procedure approach;
                            if (!airport.Approaches.ContainsKey(procedureIdent))
                            {
                                approach = new Procedure(procedureIdent);
                                airport.Approaches.Add(approach.Ident, approach);
                            }
                            else
                            {
                                approach = airport.Approaches[procedureIdent];
                            }

                            switch (approachProcedureRecord.RouteType)
                            {
                                case 'A':
                                {
                                    Transition transition;
                                    if (!approach.Transitions.ContainsKey(approachProcedureRecord.TransitionIdent))
                                    {
                                        transition = new Transition(approachProcedureRecord.TransitionIdent);
                                        approach.Transitions.Add(approachProcedureRecord.TransitionIdent, transition);
                                    }
                                    else
                                    {
                                        transition = approach.Transitions[approachProcedureRecord.TransitionIdent];
                                    }
                                    break;
                                }
                            }
                            
                            break;
                        }
                    }
                }
            }

            var sb = new StringBuilder();
            foreach (var airport in airports.Values)
            {
                if (airport.Waypoints.Count == 0)
                    continue;
                
                PrintAirport(airport, sb);
            }
            
            File.WriteAllText("C:/Users/evgeny.vasilyev/Downloads/CIFP_210128/out.txt", sb.ToString());
        }

        static void PrintAirport(Airport airport, StringBuilder sb)
        {
            sb.AppendLine($"{airport.Ident}");
            sb.AppendLine("├── Waypoints");

            var waypointsCount = airport.Waypoints.Count;
            var waypoints = airport.Waypoints.Values.ToList();
            for (var i = 0; i < waypointsCount - 1; i++)
            {
                var waypoint = waypoints[i];
                sb.AppendLine($"│   ├── {waypoint.Ident}");
            }

            if (waypointsCount > 0)
            {
                var lastWaypoint = waypoints[waypointsCount - 1];
                sb.AppendLine($"│   └── {lastWaypoint.Ident}");
            }
            
            sb.AppendLine("└── Approaches");
            var approachesCount = airport.Approaches.Count;
            var approaches = airport.Approaches.Values.ToList();
            for (var i = 0; i < approachesCount - 1; i++)
            {
                var approach = approaches[i];
                sb.AppendLine($"    ├── {approach.Ident}");
                PrintTransitions(approach, sb);
            }

            if (approachesCount > 0)
            {
                var lastApproach = approaches[approachesCount - 1];
                sb.AppendLine($"    └── {lastApproach.Ident}");
                PrintTransitions(lastApproach, sb);
            }

            sb.AppendLine();
        }

        static void PrintTransitions(Procedure approach, StringBuilder sb)
        {
            sb.AppendLine("    │   └── Transitions");
            var transitionsCount = approach.Transitions.Count;
            var transitions = approach.Transitions.Values.ToList();
            for (var i = 0; i < transitionsCount - 1; i++)
            {
                var transition = transitions[i];
                sb.AppendLine($"    │       ├── {transition.Ident}");
            }

            if (transitionsCount > 0)
            {
                var lastTransition = transitions[transitionsCount - 1];
                sb.AppendLine($"    │       └── {lastTransition.Ident}");
            }
        }
    }
}