namespace ARINC_424_Encoder
{
    public class AirportProcedureRecord : AirportBaseRecord
    {
        public string ProcedureIdent { get; set; }
        public char RouteType { get; set; }
        public string TransitionIdent { get; set; }
        public int SequenceNumber { get; set; }
        public string FixIdent { get; set; }
        public string FixIcaoCode { get; set; }
        
        public AirportProcedureRecord(AirportRecordHeader header) : base(header)
        {
        }
    }
}