using System.ComponentModel.DataAnnotations;

namespace ChemicalPropertiesApp.ViewModels;

public class RegisterModel
{
    [Display(Name = "Электронная почта")]
    [EmailAddress]
    [Required(ErrorMessage = "Не указана электронная почта")]
    public string Email { get; set; } = null!;
    
    [Display(Name = "Логин")]
    [Required(ErrorMessage = "Не указан логин")]
    public string Login { get; set; } = null!;
    
    [Display(Name = "Имя")]
    [Required(ErrorMessage = "Не указано имя")]
    public string FirstName { get; set; } = null!;
    
    [Display(Name = "Фамилия")]
    [Required(ErrorMessage = "Не указана фамилия")]
    public string LastName { get; set; } = null!;
    
    [Display(Name = "Пароль")]
    [Required(ErrorMessage = "Не указан пароль")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
    
    [Display(Name = "Подтверждение пароля")]
    [Required(ErrorMessage = "Не указано подтверждение пароля")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    public string ConfirmPassword { get; set; } = null!;
}