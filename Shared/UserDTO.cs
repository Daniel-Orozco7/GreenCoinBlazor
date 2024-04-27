using System.ComponentModel.DataAnnotations;

namespace GreenCoinHealth.Shared
{
    public class UserDTO
    {
        [Required(ErrorMessage = "El DNI es obligatorio.")]
        public string Dni { get; set; }

        [Required(ErrorMessage = "El Tipo de DNI es obligatorio.")]
        public string TypeDni { get; set; }

        [Required(ErrorMessage = "El Nombre es obligatorio.")]
        [MinLength(3, ErrorMessage = "El nombre debe ser de mínimo 3 caracteres"), StringLength(80, ErrorMessage = "El nombre debe ser de máximo 80 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El Apellido es obligatorio.")]
        [MinLength(3, ErrorMessage = "El apellido debe ser de mínimo 3 caracteres"), StringLength(80, ErrorMessage = "El apellido debe ser de máximo 80 caracteres")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [MinLength(3, ErrorMessage = "El nombre de usuario debe ser de mínimo 3 caracteres"), StringLength(50, ErrorMessage = "El nombre debe ser de máximo 50 caracteres")]
        public string username { get; set; }

        [Required(ErrorMessage = "El Email es obligatorio.")]
        public string Email { get; set; }

        [MinLength(8, ErrorMessage = "El teléfono debe ser de mínimo 8 dígitos")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "El género es obligatorio.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "El password es obligatorio.")]
        [MinLength(8, ErrorMessage = "El password debe ser de mínimo 8 caracteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "La confirmación del password es obligatoria.")]
        [MinLength(8, ErrorMessage = "La confirmación del password debe ser de mínimo 8 caracteres")]
        public string Confirm_Password { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio.")]
        [MinLength(1, ErrorMessage = "El rol debe ser de mínimo 1 caracter")]
        public string UserRole { get; set; }

    }
}
