namespace IS4
{
    public sealed class RolePreview
    {
        public string Id { get; set; }

        public string RoleName { get; set; }

        public static explicit operator RolePreview(Role role)
        {
            if (role != null)
            {
                return new RolePreview
                {
                    Id = role.Id,
                    RoleName = role.RoleType.ToString()
                };
            }
            return null;
        }
    }
}
