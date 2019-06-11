using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using SharpArch.Domain.DomainModel;

namespace Oulanka.Domain.Models
{
    public class Usuario : Entity
    {
        public virtual string NombreUsuario { get; set; }
        public virtual string NombreMostrar { get; set; }
        public virtual string Email { get; set; }
        public virtual DateTime CreadoEn { get; set; }
        public virtual DateTime ActualizadoEn { get; set; }
        public virtual int CuentaAccesosFallidos { get; set; }
        public virtual DateTime UltimoLoginEn { get; set; }
        public virtual DateTime UltimaActividadEn { get; set; }
        public virtual bool EsLdap { get; set; }
        public virtual string LocalPassword { get; set; }
        public virtual bool EstaEnLinea { get; set; }
        public virtual string EstaEnLineaLabel => EstaEnLinea ? "Conectado" : "Desconectado";
        public virtual bool EstaBloqueado { get; set; }
        
        public virtual PerfilUsuario PerfilUsuario { get; set; }
        
        [ScriptIgnore]
        public virtual ICollection<Grupo> Grupos { get; set; }
        public virtual bool EsAdmin => EstaEnGrupo("admins");
        public virtual bool EstaEnGrupo(string groupName)
        {
            return Grupos != null && 
                Grupos.Any(x => x.Nombre == groupName);
        }

        public virtual bool EsAdminUOperador()
        {
            return EstaEnGrupo("admins") || EstaEnGrupo("operators");
        }
    }
}