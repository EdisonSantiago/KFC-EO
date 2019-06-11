using System.ComponentModel.DataAnnotations;
using Oulanka.Domain.Models;

namespace Oulanka.Web.Core.FormModels
{
    public class ChangePasswordModel
    {
        public Usuario Usuario { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "Ingrese la nueva contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirmar Contraseña")]
        [Required(ErrorMessage = "Ingrese la confirmación de la contraseña")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Las contraseñas deben ser iguales")]
        public string PasswordConfirm { get; set; }



        [Display(Name = "Contraseña Actual")]
        [Required(ErrorMessage = "Ingrese la contraseña actual")]
        [DataType(DataType.Password)]

        public string OldPassword { get; set; }
    }
}