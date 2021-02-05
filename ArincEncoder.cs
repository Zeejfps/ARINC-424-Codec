namespace ARINC_424_Encoder
{
    public abstract class ArincEncoder
    {
        public abstract IRecord DecodeRecord(string encodedRecord);
    }
}