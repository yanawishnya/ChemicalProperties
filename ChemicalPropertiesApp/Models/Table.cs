namespace ChemicalPropertiesApp.Models;

public class Table
{
    public List<SolidPhase> solidPhaseTable { get; set; }
    public List<PhaseEquilibrium> phaseEquilibriaTable { get; set; }
    public List<PhaseEquilibriaDetail> phaseEquilibriaDetailTable { get; set; }
    public List<ThermodynamicDataActivity> thermodynamicDataActivityTable { get; set; }
    public List<ThermodynamicDataEnthalpy> thermodynamicDataEnthalphyTable { get; set; }
    public List<ThermodynamicDataHeatCapacity> thermodynamicDataCapacityTable { get; set; }
}