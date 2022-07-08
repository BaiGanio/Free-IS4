using System.Collections.Generic;
using System.Linq;

namespace IS4
{
    public class UserView
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        
        public bool IsConfirmed { get; set; }

        public string TypeOfUser { get; set; }

        public List<RolePreview> Roles { get; set; }

        public static explicit operator UserView(User user)
        {
            if (user != null)
            {
                List<RolePreview> roles = GetRoles(user);
                return new UserView
                {
                    Id = user.Id,
                    Email = user.Email,
                    Password = user.Password,
                    IsConfirmed = user.IsConfirmed,
                    TypeOfUser = user.TypeOfUser.ToString(),
                    Roles = roles
                };
            }
            return null;
        }
        private static List<RolePreview> GetRoles(User user)
        {
            return
                user.Roles
                    .Select(r =>
                        new RolePreview
                        {
                            Id = r.RoleId,
                            RoleName = r.Role.RoleType.ToString()
                        }
                    ).ToList();
        }
    }
}
