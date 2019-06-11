using SharpArch.Domain.DomainModel;

namespace Oulanka.Domain.Models
{
    public class Enumeration : Entity 
    {
        public virtual string OptionGroup { get; set; }
        public virtual string OptionName { get; set; }
        public virtual string Name { get; set; }
        public virtual string Value { get; set; }
        public virtual int Position { get; set; }
        public virtual bool IsDefault { get; set; }

    }
}