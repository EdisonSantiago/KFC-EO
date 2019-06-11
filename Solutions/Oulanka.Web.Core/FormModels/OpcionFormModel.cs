using System;

namespace Oulanka.Web.Core.FormModels
{
    public class OpcionFormModel
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public virtual string Etiqueta { get; set; }
        public virtual string Valor { get; set; }
        public virtual short TipoOpcion { get; set; }

        public Guid EstadoId { get; set; }
    }
}