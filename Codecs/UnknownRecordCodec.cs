namespace ARINC_424_Encoder
{
    public class UnknownRecordCodec : RecordCodec
    {
        public override BaseRecord Decode(RecordHeader header, string encodedRecord)
        {
            return new UnknownRecord(header);
        }
    }
}