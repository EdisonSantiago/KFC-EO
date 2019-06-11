using System;
using SharpArch.Domain.DomainModel;

namespace Oulanka.Domain.Models
{
    public class ProjectMember : Entity 
    {
        public virtual Usuario Usuario { get; set; }
        public virtual Grupo Grupo { get; set; }

        public virtual Project Project { get; set; }

        public virtual DateTime CreatedOn { get; set; }
        public virtual string CreatedBy { get; set; }
    }
}