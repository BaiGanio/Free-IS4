using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace IS4
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LeadSource
    {
        [EnumMember(Value = "Postman")]
        Postman = 1,
        [EnumMember(Value = "WebApp")]
        WebApp = 2,
        [EnumMember(Value = "Seed")]
        Seed = 3
    }
}
