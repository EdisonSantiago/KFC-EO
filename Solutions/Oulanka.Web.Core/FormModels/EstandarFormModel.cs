using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Oulanka.Web.Core.Models;

namespace Oulanka.Web.Core.FormModels
{
    public class EstandarFormModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Codigo")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }

        [DataType(DataType.MultilineText)]
        public string NotasEspeciales { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Grupo de Estandar")]
        public Guid GrupoEstandar { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Tipo de Estandar")]
        public short TipoEstandar { get; set; }
        
        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Estado")]
        public  Guid Estado { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Nivel")]
        public  Guid Nivel { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Categoría")]
        public  Guid Categoria { get; set; }
        
        [Display(Name = "Tipo de Estandar")]
        public Guid? Pariente { get; set; }

         
        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Clasificacion")]
        public Guid Clasificacion { get; set; }
        
        [Display(Name="Sistema")]
        public string[] Sistema { get; set; }

        [Display(Name="TipoLocal")]
        public string[] TipoLocal { get; set; }

        public List<SelectListItem> Estados { get; set; }
        public List<SelectListItem> GrupoEstandares { get; set; }
        public List<SelectListItem> TipoEstandares { get; set; }
        public List<SelectListItem> Niveles { get; set; }
        public List<SelectListItem> Categorias { get; set; }
        public List<SelectListItem> Clasificaciones { get; set; }
        public CheckBoxList Sistemas { get; set; }
        public CheckBoxList TipoLocales { get; set; }

        public EstandarFormModel()
        {
            Estados = new List<SelectListItem>();
            GrupoEstandares = new List<SelectListItem>();
            Niveles = new List<SelectListItem>();
            Categorias = new List<SelectListItem>();
            Clasificaciones = new List<SelectListItem>();
            Sistemas = new CheckBoxList();
            TipoLocales = new CheckBoxList();
        }
    }
}