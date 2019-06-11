using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Oulanka.Web.Core.FormModels
{
    public class ProvinciaFormModel
    {
        public virtual Guid Id { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Nombre")]
        public virtual string Nombre { get; set; }
        
        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Region")]
        public virtual Guid Region { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Estado")]
        public virtual Guid Estado { get; set; }

        public List<SelectListItem> Estados { get; set; }
        public List<SelectListItem> Regiones{ get; set; }

        public ProvinciaFormModel()
        {
            Estados = new List<SelectListItem>();
            Regiones = new List<SelectListItem>();
        }
    }
}