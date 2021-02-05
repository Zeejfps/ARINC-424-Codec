namespace ARINC_424_Encoder
{
    public class AirportProcedureRecordCodec : AirportRecordCodec
    {
        protected override BaseRecord Decode(AirportRecordHeader header, string encodedRecord)
        {
            AirportProcedureRecord record;
            switch (header.SubsectionCode)
            {
                case 'F':
                    record = new AirportApproachProcedureRecord(header);
                    break;
                default:
                    record = new AirportProcedureRecord(header);
                    break;
            }

            record.ProcedureIdent = encodedRecord.Substring(13, 6);
            record.RouteType = encodedRecord[19];
            record.TransitionIdent = encodedRecord.Substring(20, 5);
            record.SequenceNumber = int.Parse(encodedRecord.Substring(26, 3));
            record.FixIdent = encodedRecord.Substring(29, 5);
            record.FixIcaoCode = encodedRecord.Substring(34, 2);
            
            return record;
        }
    }
}