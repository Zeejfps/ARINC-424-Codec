namespace ARINC_424_Encoder
{
    public class AirportTerminalWaypointRecord : AirportBaseRecord
    {
        public string WaypointIdent { get; set; }
        public string WaypointIcaoCode { get; set; }
        public string WaypointDescription { get; set; }
        
        public AirportTerminalWaypointRecord(AirportRecordHeader header) : base(header)
        {
        }
    }
}