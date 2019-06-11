using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Oulanka.Web.Core.FormModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Nombre de usuario es requerido")]
        [Display(Name = "Nombre de Usuario")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Contraseña es requerido")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "Recordarme")]
        public bool RememberMe { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un proyecto")]
        [Display(Name ="Proyecto")]
        public int ProjectId { get; set; }

        public List<SelectListItem> Projects { get; set; }
    }
}