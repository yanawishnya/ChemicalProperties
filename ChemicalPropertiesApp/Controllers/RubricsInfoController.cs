using System.Globalization;
using ChemicalPropertiesApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChemicalPropertiesApp.Controllers;

public class RubricsInfoController : Controller
{
    private MisisContext _misisContext;

    public RubricsInfoController(MisisContext context)
    {
        _misisContext = context;
    }

    [Authorize]
    public IActionResult Index(string searchString)
    {
        var rubricsInfos = (from rubricsInfo in _misisContext.RubricsInfos select rubricsInfo);
        var typesInfos = (from typesInfo in _misisContext.TypesInfos select typesInfo);
        if (!string.IsNullOrEmpty(searchString))
        {
            rubricsInfos = rubricsInfos.Where(s => s.RubricName.Contains(searchString));
        }
        var rubrics = new Rubric
        {
            rubricsInfos = rubricsInfos,
            typesInfos = typesInfos
        };
        return View(rubrics);
    }

    [Authorize(Roles = "privileged")]
    [HttpPost]
    public IActionResult Export()
    {
        var rubricsInfos = (from rubricsInfo in _misisContext.RubricsInfos.ToList()
            select new []
            {
                rubricsInfo.Created.ToString(CultureInfo.CurrentCulture),
                rubricsInfo.RubricName,
                rubricsInfo.Type.ToString()
            }).ToList<object>();

        rubricsInfos.Insert(0, new[] { "Created", "RubricName", "Type" });

        var sb = new System.Text.StringBuilder();
        for (var i = 0; i < rubricsInfos.Count; i++)
        {
            var rubricsInfo = (string[])rubricsInfos[i];
            foreach (var t in rubricsInfo)
            {
                sb.Append(t + ',');
            }

            sb.Append("\r\n");
        }

        return File(System.Text.Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "Grid.csv");
    }
}