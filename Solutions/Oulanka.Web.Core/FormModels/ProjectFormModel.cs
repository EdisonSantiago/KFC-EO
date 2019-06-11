using System.ComponentModel.DataAnnotations;

namespace Oulanka.Web.Core.FormModels
{
    public class ProjectFormModel
    {
        [Required(ErrorMessage = "Es requerido")]
        [Display(Name = "Identificador")]
        public string Identifier { get; set; }

        [Required(ErrorMessage = "Es requerido")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Es requerido")]
        [Display(Name = "Descripción")]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        public int Id { get; set; }
    }
}