using System;
using System.Collections.Generic;

namespace ChemicalPropertiesApp.Models
{
    public partial class ThermodynamicDataEnthalpy
    {
        public int EnthalpyId { get; set; }
        public string Reaction { get; set; } = null!;
        public double Temperature1 { get; set; }
        public double? Temperature2 { get; set; }
        public double? Enthalpy { get; set; }
        public double? EnthalpyAccuracy { get; set; }
        public string? Comment { get; set; }

        public virtual ObjectsInfo ObjectsInfo { get; set; } = null!;
    }
}
