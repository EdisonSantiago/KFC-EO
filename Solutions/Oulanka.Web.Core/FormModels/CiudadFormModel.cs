using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Oulanka.Web.Core.FormModels
{
    public class CiudadFormModel
    {
        public virtual Guid Id { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Nombre")]
        public virtual string Nombre { get; set; }
        
        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Provincia")]
        public virtual Guid Provincia { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Estado")]
        public virtual Guid Estado { get; set; }

        public List<SelectListItem> Estados { get; set; }
        public List<SelectListItem> Provincias{ get; set; }

        public CiudadFormModel()
        {
            Estados = new List<SelectListItem>();
            Provincias = new List<SelectListItem>();
        }
    }
}