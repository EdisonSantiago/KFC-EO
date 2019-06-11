using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Oulanka.Web.Core.FormModels
{
    public class EquipoFormModel
    {
        public virtual Guid Id { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Modelo")]
        public virtual string Modelo { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Descripción")]
        [DataType(DataType.Date)]
        public virtual string Descripcion { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Utilidad")]
        public virtual short Utilidad { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Control")]
        public virtual short Control { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Cantidad")]
        public virtual int Cantidad { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Estado")]
        public virtual Guid Estado { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "TipoEquipo")]
        public virtual Guid TipoEquipo { get; set; }

        public List<SelectListItem> Estados { get; set; }
        public List<SelectListItem> TiposEquipos { get; set; }

        public EquipoFormModel()
        {
            Estados = new List<SelectListItem>();
            TiposEquipos = new List<SelectListItem>();
        }
    }
}