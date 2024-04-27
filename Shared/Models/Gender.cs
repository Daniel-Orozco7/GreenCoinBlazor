using System;
using System.Collections.Generic;

namespace GreenCoinHealth.Shared.Models
{
    public partial class Gender
    {
        public Gender()
        {
            Users = new HashSet<User>();
        }

        public string IdGender { get; set; } = null!;
        public string NameGender { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }
    }
}
