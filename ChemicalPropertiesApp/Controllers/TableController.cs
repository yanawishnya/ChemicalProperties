using System.Globalization;
using ChemicalPropertiesApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChemicalPropertiesApp.Controllers;

public class TableController : Controller
{
    private MisisContext _misisContext;

    public TableController(MisisContext context)
    {
        _misisContext = context;
    }

    [Authorize]
    public IActionResult Index()
    {
        var solidPhaseTables = (from solidPhaseTable in _misisContext.SolidPhases select solidPhaseTable).ToList();
        var phaseEquilibriaTables =
            (from phaseEquilibriaTable in _misisContext.PhaseEquilibria select phaseEquilibriaTable).ToList();
        var phaseEquilibriaDetailTables = (from phaseEquilibriaDetailTable in _misisContext.PhaseEquilibriaDetails
            select phaseEquilibriaDetailTable).ToList();
        var thermodynamicDataActivityTables =
            (from thermodynamicDataActivityTable in _misisContext.ThermodynamicDataActivities
                select thermodynamicDataActivityTable).ToList();
        var thermodynamicDataEnthalphyTables =
            (from thermodynamicDataEnthalphyTable in _misisContext.ThermodynamicDataEnthalpies
                select thermodynamicDataEnthalphyTable).ToList();
        var thermodynamicDataHeatCapacityTables =
            (from thermodynamicDataHeatCapacityTable in _misisContext.ThermodynamicDataHeatCapacities
                select thermodynamicDataHeatCapacityTable).ToList();
        var tables = new Table
        {
            SolidPhaseTable = solidPhaseTables,
            PhaseEquilibriaTable = phaseEquilibriaTables,
            PhaseEquilibriaDetailTable = phaseEquilibriaDetailTables,
            ThermodynamicDataActivityTable = thermodynamicDataActivityTables,
            ThermodynamicDataEnthalphyTable = thermodynamicDataEnthalphyTables,
            ThermodynamicDataCapacityTable = thermodynamicDataHeatCapacityTables
        };
        return View(tables);
    }

    [Authorize(Roles = "privileged")]
    [HttpPost]
    public IActionResult ExportSolid()
    {
        var solidPhaseTable = (from solidPhase in _misisContext.SolidPhases.ToList()
            select new[]
            {
                solidPhase.Formula,
                solidPhase.Temperature1.ToString(),
                solidPhase.Temperature2.ToString(),
                solidPhase.PearsonSymbol,
                solidPhase.SpaceGroup,
                solidPhase.Prototype,
                solidPhase.LatticeA.ToString(),
                solidPhase.LatticeAlpha.ToString(),
                solidPhase.LatticeB.ToString(),
                solidPhase.LatticeBeta.ToString(),
                solidPhase.LatticeC.ToString(),
                solidPhase.LatticeGamma.ToString(),
                solidPhase.Comment
            }).ToList<object>();

        solidPhaseTable.Insert(0, new[]
        {
            "Formula", "Temperature 1", "Temperature 2", "Pearson Symbol", "Space Group", "Prototype", "Lattice A",
            "Lattice Alpha", "Lattice B", "Lattice Betta", "Lattice C", "Lattice Gamma"
        });

        var sb = new System.Text.StringBuilder();
        for (var i = 0; i < solidPhaseTable.Count; i++)
        {
            var solidPhases = (string[])solidPhaseTable[i];
            foreach (var t in solidPhases)
            {
                sb.Append(t + ',');
            }

            sb.Append("\r\n");
        }

        return File(System.Text.Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "SolidPhaseTable.csv");
    }

    //TODO: изменить вид таблицы и раскомментировать этот кусок.
    /*[Authorize(Roles = "privileged")]
    [HttpPost]
    public IActionResult ExportEquilibrium()
    {
        var equilibriumTable = (from equilibrium in _misisContext.PhaseEquilibria.ToList()
            select new[]
            {
                equilibrium.Formula,
                equilibrium.Temperature1.ToString(),
                equilibrium.Temperature2.ToString(),
                equilibrium.PearsonSymbol,
                equilibrium.SpaceGroup,
                equilibrium.Prototype,
                equilibrium.LatticeA.ToString(),
                equilibrium.LatticeAlpha.ToString(),
                equilibrium.LatticeB.ToString(),
                equilibrium.LatticeBeta.ToString(),
                equilibrium.LatticeC.ToString(),
                equilibrium.LatticeGamma.ToString(),
                equilibrium.Comment
            }).ToList<object>();

        equilibriumTable.Insert(0, new[]
        {
            "Formula", "Temperature 1", "Temperature 2", "Pearson Symbol", "Space Group", "Prototype", "Lattice A",
            "Lattice Alpha", "Lattice B", "Lattice Betta", "Lattice C", "Lattice Gamma"
        });

        var sb = new System.Text.StringBuilder();
        for (var i = 0; i < equilibriumTable.Count; i++)
        {
            var solidPhases = (string[])equilibriumTable[i];
            foreach (var t in solidPhases)
            {
                sb.Append(t + ',');
            }

            sb.Append("\r\n");
        }

        return File(System.Text.Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "SolidPhaseTable.csv");
    }*/
    
    [Authorize(Roles = "privileged")]
    [HttpPost]
    public IActionResult ExportEnthalpy()
    {
        var enthalpyTable = (from enthalpy in _misisContext.ThermodynamicDataEnthalpies.ToList()
            select new[]
            {
                enthalpy.Reaction,
                enthalpy.Temperature1.ToString(CultureInfo.CurrentCulture),
                enthalpy.Temperature2.ToString(),
                enthalpy.Enthalpy.ToString(),
                enthalpy.Comment
            }).ToList<object>();

        enthalpyTable.Insert(0, new[]
        {
            "Reaction", "Temperature 1", "Temperature 2", "Enthalpy", "Comment"
        });

        var sb = new System.Text.StringBuilder();
        for (var i = 0; i < enthalpyTable.Count; i++)
        {
            var enthalpies = (string[])enthalpyTable[i];
            foreach (var t in enthalpies)
            {
                sb.Append(t + ',');
            }

            sb.Append("\r\n");
        }

        return File(System.Text.Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "EnthalpyTable.csv");
    }

    [Authorize(Roles = "privileged")]
    [HttpPost]
    public IActionResult ExportActivity()
    {
        var activityTable = (from activity in _misisContext.ThermodynamicDataActivities.ToList()
            select new[]
            {
                activity.Element,
                activity.Temperature.ToString(CultureInfo.CurrentCulture),
                activity.Composition.ToString(CultureInfo.CurrentCulture),
                activity.Activity.ToString(CultureInfo.CurrentCulture),
                activity.Comment
            }).ToList<object>();

        activityTable.Insert(0, new[]
        {
            "Element", "Temperature", "Composition", "Activity", "Comment"
        });

        var sb = new System.Text.StringBuilder();
        for (var i = 0; i < activityTable.Count; i++)
        {
            var activities = (string[])activityTable[i];
            foreach (var t in activities)
            {
                sb.Append(t + ',');
            }

            sb.Append("\r\n");
        }

        return File(System.Text.Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "ActivityTable.csv");
    }

    [Authorize(Roles = "privileged")]
    [HttpPost]
    public IActionResult ExportHeatCapacity()
    {
        var heatCapacityTable = (from heatCapacity in _misisContext.ThermodynamicDataHeatCapacities.ToList()
            select new[]
            {
                heatCapacity.Phase,
                heatCapacity.Temperature.ToString(CultureInfo.CurrentCulture),
                heatCapacity.HeatCapacity.ToString(CultureInfo.CurrentCulture),
                heatCapacity.Comment
            }).ToList<object>();

        heatCapacityTable.Insert(0, new[]
        {
            "Phase", "Temperature", "Heat Capacity", "Comment"
        });

        var sb = new System.Text.StringBuilder();
        for (var i = 0; i < heatCapacityTable.Count; i++)
        {
            var heatCapacities = (string[])heatCapacityTable[i];
            foreach (var t in heatCapacities)
            {
                sb.Append(t + ',');
            }

            sb.Append("\r\n");
        }

        return File(System.Text.Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "HeatCapacityTable.csv");
    }
}