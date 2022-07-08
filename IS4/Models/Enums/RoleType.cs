using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace IS4
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RoleType
    {
        [EnumMember(Value = "Member")]
        Member = 1,
        [EnumMember(Value = "Admin")]
        Admin = 2,
        [EnumMember(Value = "Student")]
        Student = 3,
        [EnumMember(Value = "Teacher")]
        Teacher = 4,
        [EnumMember(Value = "Blogger")]
        Blogger = 5
    }
}
