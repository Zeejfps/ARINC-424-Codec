namespace ARINC_424_Encoder
{
    public interface IRecord {}
    
    public class RecordHeader
    {
        public char Type { get; set; }
        public string AreaCode { get; set; }
        public char SectionCode { get; set; }
        public char SubsectionCode { get; set; }

        public RecordHeader(RecordHeader copy)
        {
            Type = copy.Type;
            AreaCode = copy.AreaCode;
            SectionCode = copy.SectionCode;
            SubsectionCode = copy.SubsectionCode;
        }
        
        public RecordHeader() {}
    }

    public class RecordFooter
    {
        public string FileRecordNumber { get; set; }
        public string CycleDate { get; set; }
    }
    
    public abstract class BaseRecord : IRecord
    {
        public char Type => Header.Type;
        public string AreaCode => Header.AreaCode;
        public char SectionCode => Header.SectionCode;
        public char SubsectionCode => Header.SubsectionCode;
        
        RecordHeader Header { get; set; }
        RecordFooter Footer { get; set; }
        
        public BaseRecord(RecordHeader header)
        {
            Header = header;
        }
    }
}