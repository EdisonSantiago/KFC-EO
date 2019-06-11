using System.ComponentModel.DataAnnotations;

namespace Oulanka.Web.Core.FormModels
{
    public class UserAdminFormModel
    {
        [Required(ErrorMessage = "Requerido")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [EmailAddress(ErrorMessage = "Debe ingresar una dirección de correo electrónico")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [StringLength(100, ErrorMessage =
        "La {0} debe tener al menos {2} caracteres de largo.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        [Compare("Password", ErrorMessage =
        "La Contraseña y su Confirmación no coinciden")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [DataType(DataType.PhoneNumber,ErrorMessage = "Debe ser un número telefónico")]
        public string Phone { get; set; }
        

    }
}