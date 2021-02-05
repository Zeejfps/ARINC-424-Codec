namespace ARINC_424_Encoder
{
    public class AirportReferencePointCodec : AirportRecordCodec
    {
        protected override BaseRecord Decode(AirportRecordHeader header, string encodedRecord)
        {
            var record = new AirportReferencePointRecord(header)
            {
                AtaDesignator = encodedRecord.Substring(13, 3),
                LatitudeDMS = encodedRecord.Substring(32, 9),
                LongitudeDMS = encodedRecord.Substring(41, 10),
                AirportName = encodedRecord.Substring(93, 30),
            };
            return record;
        }
    }
}