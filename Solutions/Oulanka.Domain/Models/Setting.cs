using System;
using SharpArch.Domain.DomainModel;

namespace Oulanka.Domain.Models
{
    public class Setting : Entity
    {
        public virtual string OptionName { get; set; }
        public virtual string Name { get; set; }
        public virtual string Value { get; set; }
        public virtual DateTime CreatedOn { get; set; }
        public virtual DateTime UpdatedOn { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual string UpdatedBy { get; set; }
    }
}