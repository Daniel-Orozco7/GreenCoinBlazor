using System.ComponentModel.DataAnnotations;

namespace GreenCoinHealth.Shared
{
    public class UserDTO
    {
        [Required]
        public string Dni { get; set; }
        [Required]
        public string TypeDni { get; set; }
        [Required]
        [MinLength(3), StringLength(80)]
        public string Name { get; set; }
        [MinLength(3), StringLength(80)]
        public string second_name { get; set; }
        [Required]
        [MinLength(3), StringLength(80)]
        public string LastName { get; set; }
        [MinLength(3), StringLength(80)]
        public string second_surname { get; set; }
        [Required]
        [MinLength(3), StringLength(50)]
        public string username { get; set; }
        [Required]
        public string Email { get; set; }
        [MinLength(8)]
        public string Phone { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        [Required]
        [MinLength(8)]
        public string Confirm_Password { get; set; }
    }
}
