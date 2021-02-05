namespace ARINC_424_Encoder
{
    public abstract class RecordCodec
    {
        public abstract BaseRecord Decode(RecordHeader header, string encodedRecord);
    }
}