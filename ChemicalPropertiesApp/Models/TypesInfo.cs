using System;
using System.Collections.Generic;

namespace ChemicalPropertiesApp.Models
{
    public partial class TypesInfo
    {
        public TypesInfo()
        {
            RubricsInfos = new HashSet<RubricsInfo>();
        }

        public int TypeId { get; set; }
        public bool IsHierarchical { get; set; }
        public string TypeName { get; set; } = null!;
        public string? TypeComment { get; set; }

        public virtual ICollection<RubricsInfo> RubricsInfos { get; set; }
    }
}
