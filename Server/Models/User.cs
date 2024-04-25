using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GreenCoinHealth.Server.Models
{
    public partial class User
    {
        [Required]
        public string Dni { get; set; } = null!;
        [Required]
        public string TypeDni { get; set; } = null!;
        [Required]
        [MinLength(3)]
        public string Name { get; set; } = null!;
        [Required]
        [MinLength(3)]
        public string LastName { get; set; } = null!;
        [Required]
        [MinLength(3)]
        public string Username { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        [MinLength(8)]
        public string Password { get; set; } = null!;
        [Required]
        public string Phone { get; set; } = null!;
        public string IdGender { get; set; } = null!;
        public string IdRole { get; set; } = null!;

        public virtual Gender IdGenderNavigation { get; set; } = null!;
        public virtual Role IdRoleNavigation { get; set; } = null!;
    }
}
