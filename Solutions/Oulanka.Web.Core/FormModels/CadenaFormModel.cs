using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace Oulanka.Web.Core.FormModels
{
    public class CadenaFormModel
    {
        public  Guid Id { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Nombre")]
        public  string Nombre { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Descripción")]
        public  string Descripcion { get; set; }

        [Display(Name = "Manual")]
        [DataType(DataType.Upload)]
        public  HttpPostedFileBase Manual { get; set; }

        public string ManualUrl { get; set; }

        [Display(Name = "Logotipo")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase Logo { get; set; }

        public  string LogoUrl { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Fecha de Fundación")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public  DateTime FechaFundacion { get; set; }
        
        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Estado")]
        public  Guid Estado { get; set; }

        public List<SelectListItem> Estados { get; set; }

        public CadenaFormModel()
        {
            Estados = new List<SelectListItem>();
        }
    }
}