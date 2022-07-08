using System;

namespace IS4
{
    public sealed class UserRole
    {
        public UserRole() { }
        public UserRole(
          string userId,
          string roleId,
          DateTime? createdOn = null
        )
        {
            UserId = userId;
            RoleId = roleId;
            CreatedOn = createdOn ?? DateTime.Now;
        }

        public string UserId { get; private set; }

        public User User { get; private set; }

        public string RoleId { get; private set; }

        public Role Role { get; private set; }

        public DateTime? CreatedOn { get; private set; }
    }
}
