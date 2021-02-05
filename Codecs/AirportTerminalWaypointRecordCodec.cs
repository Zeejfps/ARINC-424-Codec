namespace ARINC_424_Encoder
{
    public class AirportTerminalWaypointRecordCodec : AirportRecordCodec
    {
        protected override BaseRecord Decode(AirportRecordHeader header, string encodedRecord)
        {
            var record = new AirportTerminalWaypointRecord(header)
            {
                WaypointIdent = encodedRecord.Substring(13, 5),
                WaypointIcaoCode = encodedRecord.Substring(19, 2),
                WaypointDescription = encodedRecord.Substring(98, 25),
            };
            return record;
        }
    }
}