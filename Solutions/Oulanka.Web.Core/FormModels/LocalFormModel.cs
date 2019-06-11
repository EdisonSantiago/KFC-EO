using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace Oulanka.Web.Core.FormModels
{
    public class LocalFormModel
    {
        public  Guid Id { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Codigo")]
        public  string Codigo { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Nombre")]
        public  string Nombre { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        public  string Descripcion { get; set; }

        [Display(Name = "Imagen")]
        [DataType(DataType.Upload)]
        public  HttpPostedFileBase Imagen { get; set; }
        
        public string ImagenUrl { get; set; }

        [Display(Name = "Logo")]
        [DataType(DataType.Upload)]
        public  HttpPostedFileBase Logo { get; set; }

        public string LogoUrl { get; set; }


        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Direccion")]
        [DataType(DataType.MultilineText)]
        public  string Direccion { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Telefono")]
        [DataType(DataType.PhoneNumber)]
        public  string Telefono { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public  string Email { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "R.U.C.")]
        public  string Ruc { get; set; }

        [Display(Name = "OpClave")]
        public  string OpClave { get; set; }

        [Display(Name = "Propietario")]
        public  string Propietario { get; set; }

        [Display(Name = "AC")]
        public  string AC { get; set; }

        [Display(Name = "Concepto")]
        public  short Concepto { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Estado")]
        public  Guid Estado { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "TipoLocal")]
        public  Guid TipoLocal { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Cadena")]
        public  Guid Cadena { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Ciudad")]
        public  Guid Ciudad { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Jefe de Area")]
        public  Guid JefeArea { get; set; }

        public List<SelectListItem> Estados { get; set; }
        public List<SelectListItem> TiposLocales { get; set; }
        public List<SelectListItem> Cadenas { get; set; }
        public List<SelectListItem> Ciudades { get; set; }
        public List<SelectListItem> JefesArea { get; set; }

        public LocalFormModel()
        {
            Estados = new List<SelectListItem>();
            TiposLocales = new List<SelectListItem>();
            Cadenas = new List<SelectListItem>();
            Ciudades = new List<SelectListItem>();
            JefesArea = new List<SelectListItem>();
        }

    }
}