using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IS4
{
    public sealed class Role
    {
        private CustomId _id;

        public Role() { }
        public Role(RoleType roleType)
        {
            this.RoleType = roleType;
            this._id = new CustomId();
        }

        [Key]
        public string Id
        {
            get { return this._id.ToString(); }
            private set { this._id = new CustomId(new Guid(value)); }
        }

        public RoleType RoleType { get; private set; }

        public ICollection<UserRole> Users { get; private set; } = new HashSet<UserRole>();
    }
}
