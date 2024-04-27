using System;
using System.Collections.Generic;

namespace GreenCoinHealth.Shared.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public string IdRole { get; set; } = null!;
        public string Name { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }
    }
}
