using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using SharpArch.Domain.DomainModel;

namespace Oulanka.Domain.Models
{
    public class Project : Entity
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string Identifier { get; set; }
        public virtual bool IsPrivate { get; set; }
        public virtual DateTime CreatedOn { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime UpdatedOn { get; set; }
        public virtual string UpdateBy { get; set; }
        public virtual short Status { get; set; }

        [ScriptIgnore]
        public virtual IList<ProjectMember> Members { get; set; }
    }
}