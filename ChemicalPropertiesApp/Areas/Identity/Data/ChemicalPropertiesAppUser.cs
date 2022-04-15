using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ChemicalPropertiesApp.Areas.Identity.Data;

public class ChemicalPropertiesAppUser : IdentityUser
{
    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string Login { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string LastName { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string Patronymic { get; set; }
}