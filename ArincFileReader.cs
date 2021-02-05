using System;
using System.IO;

namespace ARINC_424_Encoder
{
    public class ArincFileReader : IDisposable
    {
        StreamReader Reader { get; }
        ArincSpecification Specification { get; }
        ArincEncoder Encoder { get; }
        
        public ArincFileReader(string pathToFile, ArincSpecification specification)
        {
            Reader = new StreamReader(File.OpenRead(pathToFile));
            Specification = specification;

            switch (Specification)
            {
                case ArincSpecification.ARINC424_17:
                    Encoder = new ARINC424_17_Encoder();
                    break;
                default:
                    Encoder = null;
                    break;
            }
        }

        public IRecord ReadRecord()
        {
            var encodedRecord = Reader.ReadLine();
            if (encodedRecord == null)
                return null;
            return Encoder.DecodeRecord(encodedRecord);
        }
        
        public void Dispose()
        {
            Reader.Dispose();
        }
    }
}