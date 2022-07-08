namespace IS4
{
    using System.Runtime.Serialization;

    public enum TypeOfUser
    {
        [EnumMember(Value = "Aplicant")]
        Aplicant = 0,
        [EnumMember(Value = "Customer")]
        Customer = 1,
        [EnumMember(Value = "Cancelled")]
        Cancelled = 99
    }
}
