using System.Collections.Generic;

namespace ARINC_424_Encoder
{
    public class ARINC424_17_Encoder : ArincEncoder
    {
        const char kBlank = ' ';

        Dictionary<(char, char), RecordCodec> Decoders { get; } =
            new Dictionary<(char, char), RecordCodec>();

        UnknownRecordCodec UnknownRecordCodec { get; } = new UnknownRecordCodec();
        
        public ARINC424_17_Encoder()
        {
            RegisterCodec('P', 'A', new AirportReferencePointCodec());
            RegisterCodec('P', 'C', new AirportTerminalWaypointRecordCodec());
            RegisterCodec('P', 'F', new AirportProcedureRecordCodec());
        }
        
        public override IRecord DecodeRecord(string encodedRecord)
        {
            var header = DecodeHeader(encodedRecord);
            var decoder = FindCodec(header.SectionCode, header.SubsectionCode);
            return decoder.Decode(header, encodedRecord);
        }

        void RegisterCodec(char sectionCode, char subsectionCode, RecordCodec codec)
        {
            Decoders.Add((sectionCode, subsectionCode), codec);
        }
        
        RecordCodec FindCodec(char sectionCode, char subsectionCode)
        {
            var key = (sectionCode, subsectionCode);
            if (Decoders.ContainsKey(key))
                return Decoders[key];
            return UnknownRecordCodec;
        }
        
        RecordHeader DecodeHeader(string encodedRecord)
        {
            var header = new RecordHeader
            {
                Type = encodedRecord[0],
                AreaCode = encodedRecord.Substring(1, 3),
                SectionCode = encodedRecord[4],
                SubsectionCode = encodedRecord[5]
            };
            if (header.SubsectionCode == kBlank)
                header.SubsectionCode = encodedRecord[12];
            
            return header;
        }
    }
}