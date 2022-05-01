namespace ChemicalPropertiesApp.Models;

public class Rubric
{
    public IQueryable<RubricsInfo>? RubricsInfos { get; set; }
    public IQueryable<TypesInfo>? TypesInfos { get; set; }
}