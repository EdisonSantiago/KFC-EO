using System;
using NHibernate.Mapping.ByCode;

namespace Oulanka.Infrastructure.NHibernateMaps.Helpers
{
    public class UUIDHexCombGeneratorDef : IGeneratorDef 
    {
        public UUIDHexCombGeneratorDef(string format)
        {
            if (format == null) throw new ArgumentNullException(nameof(format));

            Params = new { format = format };
        }


        public string Class => "uuid.hex";

        public object Params { get; }

        public System.Type DefaultReturnType => typeof(string);

        public bool SupportedAsCollectionElementId => false;
    }
}