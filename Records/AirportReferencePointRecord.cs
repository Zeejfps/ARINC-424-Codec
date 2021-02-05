namespace ARINC_424_Encoder
{
    public class AirportReferencePointRecord : AirportBaseRecord
    {
        public string AtaDesignator { get; set; }
        public string LatitudeDMS { get; set; }
        public string LongitudeDMS { get; set; }
        public string AirportName { get; set; }
        
        public AirportReferencePointRecord(AirportRecordHeader header) : base(header)
        {
        }
        
        public override string ToString()
        {
            return $"{nameof(AirportIdent)}: {AirportIdent}, {nameof(AirportIcaoCode)}: {AirportIcaoCode}, {nameof(AtaDesignator)}: {AtaDesignator}, {nameof(LatitudeDMS)}: {LatitudeDMS}, {nameof(LongitudeDMS)}: {LongitudeDMS}, {nameof(AirportName)}: {AirportName}";
        }
    }
}