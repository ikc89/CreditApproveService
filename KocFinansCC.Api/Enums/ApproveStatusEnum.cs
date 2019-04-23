namespace KocFinansCC.Api.Enums
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ApproveStatusEnum
    {
        [EnumMember(Value = "RED")]
        Rejected = 0,
        [EnumMember(Value = "KABUL")]
        Approved = 1
    }
}
