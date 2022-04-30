namespace ChemicalPropertiesApp.Models;

public class RubricsInfo
{
    public RubricsInfo()
    {
        ObjectsInfos = new HashSet<ObjectsInfo>();
        Objects = new HashSet<ObjectsInfo>();
    }

    public int RubricId { get; set; }
    public bool? IsPublished { get; set; }
    public int? ParentId { get; set; }
    public int TypeId { get; set; }
    public int Level { get; set; }
    public int Flags { get; set; }
    public DateTime Created { get; set; }
    public string RubricName { get; set; } = null!;
    public string? RubricPath { get; set; }
    public int SortType { get; set; }

    public virtual TypesInfo Type { get; set; } = null!;
    public virtual ICollection<ObjectsInfo> ObjectsInfos { get; set; }

    public virtual ICollection<ObjectsInfo> Objects { get; set; }
}