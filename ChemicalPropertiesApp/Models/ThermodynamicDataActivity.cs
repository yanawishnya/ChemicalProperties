using System;
using System.Collections.Generic;

namespace ChemicalPropertiesApp.Models
{
    public partial class ThermodynamicDataActivity
    {
        public int ActivityId { get; set; }
        public double Temperature { get; set; }
        public string Element { get; set; } = null!;
        public double Composition { get; set; }
        public double Activity { get; set; }
        public double? ActivityAccuracy { get; set; }
        public string? Comment { get; set; }

        public virtual ObjectsInfo ObjectsInfo { get; set; } = null!;
    }
}
