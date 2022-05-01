namespace ChemicalPropertiesApp.Models;

public class Table
{
    public List<SolidPhase>? SolidPhaseTable { get; set; }
    public List<PhaseEquilibrium>? PhaseEquilibriaTable { get; set; }
    public List<PhaseEquilibriaDetail>? PhaseEquilibriaDetailTable { get; set; }
    public List<ThermodynamicDataActivity>? ThermodynamicDataActivityTable { get; set; }
    public List<ThermodynamicDataEnthalpy>? ThermodynamicDataEnthalphyTable { get; set; }
    public List<ThermodynamicDataHeatCapacity>? ThermodynamicDataCapacityTable { get; set; }
}