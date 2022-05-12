using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChemicalPropertiesApp.Controllers;

public class LitReferenceController : Controller
{
    private MisisContext _misisContext;
    
    public LitReferenceController(MisisContext context)
    {
        _misisContext = context;
    }
    
    [Authorize]
    public IActionResult Index(string searchString)
    {
        var litReferences = (from litReference in _misisContext.LitReferences select litReference);
        if (!string.IsNullOrEmpty(searchString))
        {
            litReferences = litReferences.Where(s => s.Title.Contains(searchString));
        }

        return View(litReferences);
    }
    
    [Authorize(Roles = "privileged")]
    [HttpPost]
    public IActionResult Export()
    {
        var litReferences = (from litReference in _misisContext.LitReferences.ToList()
            select new[] {
                litReference.Authors,
                litReference.Title,
                litReference.Journal,
                litReference.Year.ToString(),
                litReference.Vol,
                litReference.Num,
                litReference.StartPage,
                litReference.EndPage,
                litReference.Doi,
                litReference.BibTeX
            }).ToList<object>();
 
        litReferences.Insert(0, new[] { "Authors", "Title", "Journal", "Year", "Volume", "Number", "Start Page", "End Page", "Doi", "BibTeX" });
 
        var sb = new System.Text.StringBuilder();
        for (var i = 0; i < litReferences.Count; i++)
        {
            var litReference = (string[])litReferences[i];
            foreach (var t in litReference)
            {
                sb.Append(t + ',');
            }
            
            sb.Append("\r\n");
 
        }
 
        return File(System.Text.Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "LitReferences.csv");
    }
}