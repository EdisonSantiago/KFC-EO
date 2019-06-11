using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using Oulanka.Web.Core.Models;

namespace Oulanka.Web.Core.FormModels
{
    public class GrupoEstandarFormModel
    {
        public virtual Guid Id { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Código")]
        public virtual string Codigo { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Nombre")]
        public virtual string Nombre { get; set; }

        [Display(Name = "Descripción")]
        [DataType(DataType.Date)]
        public virtual string Descripcion { get; set; }

        [Display(Name = "Imagen")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase Imagen { get; set; }

        public string ImagenUrl { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Estado")]
        public virtual Guid Estado { get; set; }


        public List<SelectListItem> Estados { get; set; }


        public GrupoEstandarFormModel()
        {
            Estados = new List<SelectListItem>();
        }
    }
}