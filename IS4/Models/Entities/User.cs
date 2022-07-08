using System.Collections.Generic;

namespace IS4
{
    public sealed class User
    {
        public string Id { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }

        public bool IsConfirmed { get; private set; }

        public TypeOfUser TypeOfUser { get; private set; }

       public ICollection<UserRole> Roles { get; } = new HashSet<UserRole>();
    }
}
