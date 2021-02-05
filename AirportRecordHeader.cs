namespace ARINC_424_Encoder
{
    public class AirportRecordHeader : RecordHeader
    {
        public string AirportIdent { get; set; }
        public string AirportIcaoCode { get; set; }

        public AirportRecordHeader(RecordHeader header) : base(header)
        {
        }
    }
}