using System;
using System.Collections.Generic;

namespace ChemicalPropertiesApp.Models
{
    public partial class PhaseEquilibriaDetail
    {
        public int PhaseEquilibriaId { get; set; }
        public int PhaseDetailId { get; set; }
        public string Element { get; set; } = null!;
        public double Composition { get; set; }
        public double? CompositionAccuracy { get; set; }

        public virtual PhaseEquilibrium PhaseEquilibria { get; set; } = null!;
    }
}
