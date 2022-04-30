using System.ComponentModel.DataAnnotations;

namespace ChemicalPropertiesApp.Models;

public class LitReference
{
    public int ReferenceId { get; set; }
    public string Authors { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? Journal { get; set; }
    public int Year { get; set; }
    public string? Vol { get; set; }
    public string? Num { get; set; }
    public string? StartPage { get; set; }
    public string? EndPage { get; set; }
    public string? Doi { get; set; }
    public string? BibTeX { get; set; }
    /// <summary>
    /// Дата последнего обновления записи
    /// </summary>
    public DateTime Date { get; set; }

    public virtual ObjectsInfo ObjectsInfo { get; set; } = null!;
}