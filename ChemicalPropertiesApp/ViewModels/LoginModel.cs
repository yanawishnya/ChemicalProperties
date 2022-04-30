using System.ComponentModel.DataAnnotations;

namespace ChemicalPropertiesApp.ViewModels;

public class LoginModel
{
    [Display(Name = "Логин")]
    [Required(ErrorMessage = "Не указан логин")]
    public string Login { get; set; } = null!;

    [Display(Name = "Пароль")]
    [Required(ErrorMessage = "Не указан пароль")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}