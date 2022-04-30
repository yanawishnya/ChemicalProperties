using System;
using System.Collections.Generic;

namespace ChemicalPropertiesApp.Models
{
    public partial class PhaseEquilibrium
    {
        public PhaseEquilibrium()
        {
            PhaseEquilibriaDetails = new HashSet<PhaseEquilibriaDetail>();
        }

        public int PhaseEquilibriaId { get; set; }
        public double Temperature { get; set; }
        public double? TemperatureAccuracy { get; set; }
        public double? AnnealedTime { get; set; }
        public string Formula { get; set; } = null!;
        public string? Comment { get; set; }

        public virtual ObjectsInfo ObjectsInfo { get; set; } = null!;
        public virtual ICollection<PhaseEquilibriaDetail> PhaseEquilibriaDetails { get; set; }
    }
}
