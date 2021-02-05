namespace ARINC_424_Encoder
{
    public class AirportBaseRecord : BaseRecord
    {
        public string AirportIdent { get; }
        public string AirportIcaoCode { get; }
        
        public AirportBaseRecord(AirportRecordHeader header) : base(header)
        {
            AirportIdent = header.AirportIdent;
            AirportIcaoCode = header.AirportIcaoCode;
        }
    }
}