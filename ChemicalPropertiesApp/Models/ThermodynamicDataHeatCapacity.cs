using System;
using System.Collections.Generic;

namespace ChemicalPropertiesApp.Models
{
    public partial class ThermodynamicDataHeatCapacity
    {
        public int HeatCapacityId { get; set; }
        public string Phase { get; set; } = null!;
        public double Temperature { get; set; }
        public double HeatCapacity { get; set; }
        public double? HeatCapacityAccuracy { get; set; }
        public string? Comment { get; set; }

        public virtual ObjectsInfo ObjectsInfo { get; set; } = null!;
    }
}
