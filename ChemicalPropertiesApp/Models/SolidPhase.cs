using System;
using System.Collections.Generic;

namespace ChemicalPropertiesApp.Models
{
    public partial class SolidPhase
    {
        public int SolidPhaseId { get; set; }
        public string Formula { get; set; } = null!;
        public string? Modification { get; set; }
        public double? Temperature1 { get; set; }
        public double? Temperature2 { get; set; }
        public string? PearsonSymbol { get; set; }
        public string? SpaceGroup { get; set; }
        public string? Prototype { get; set; }
        public double? LatticeA { get; set; }
        public double? LatticeB { get; set; }
        public double? LatticeC { get; set; }
        public double? LatticeAlpha { get; set; }
        public double? LatticeBeta { get; set; }
        public double? LatticeGamma { get; set; }
        public string? Comment { get; set; }

        public virtual ObjectsInfo ObjectsInfo { get; set; } = null!;
    }
}
