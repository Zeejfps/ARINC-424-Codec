namespace ARINC_424_Encoder
{
    public abstract class AirportRecordCodec : RecordCodec
    {
        public override BaseRecord Decode(RecordHeader header, string encodedRecord)
        {
            var airportRecordHeader = new AirportRecordHeader(header)
            {
                AirportIdent = encodedRecord.Substring(6, 4),
                AirportIcaoCode = encodedRecord.Substring(10, 2),
            };
            return Decode(airportRecordHeader, encodedRecord);
        }

        protected abstract BaseRecord Decode(AirportRecordHeader header, string encodedRecord);
    }
}