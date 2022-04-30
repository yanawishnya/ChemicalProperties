namespace ChemicalPropertiesApp.Models;

public class Rubric
{
    public IQueryable<RubricsInfo> rubricsInfos { get; set; }
    public IQueryable<TypesInfo> typesInfos { get; set; }
}