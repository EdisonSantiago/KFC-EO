using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Oulanka.Domain.Models.Locales;
using SharpArch.Domain.DomainModel;

namespace Oulanka.Domain.Models.Jerarquias
{
    public class JefeArea : EntityWithTypedId<Guid>
    {
        public virtual string Nombre { get; set; }
        public virtual string Descripcion { get; set; }

        // auditoría micro a nivel de registro
        public virtual string CreadoPor { get; set; }
        public virtual string ActualizadoPor { get; set; }
        public virtual DateTime CreadoEn { get; set; }
        public virtual DateTime ActualizadoEn { get; set; }

        public virtual Estado Estado { get; set; }
        public virtual string NombreEstado => Estado != null ? Estado.Nombre : string.Empty;

        public virtual GerenteRegional GerenteRegional { get; set; }

        [ScriptIgnore]
        public virtual IList<Local> Locales { get; set; }

        public virtual int NumLocales => Locales != null && Locales.Any() ? Locales.Count : 0;
    }
}