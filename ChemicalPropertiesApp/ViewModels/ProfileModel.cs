using System.ComponentModel.DataAnnotations;

namespace ChemicalPropertiesApp.ViewModels;

public class ProfileModel
{
    [Display(Name = "Логин")]
    [Required(ErrorMessage = "Не указан логин")]
    public string Login { get; set; } = null!;

    [Display(Name = "Пароль")]
    [Required(ErrorMessage = "Не указан пароль")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
    
    [Display(Name = "Имя")]
    [Required(ErrorMessage = "Не указано имя")]
    public string FirstName { get; set; } = null!;
    
    [Display(Name = "Отчество")]
    public string? Patronymic { get; set; }
    
    [Display(Name = "Имя")]
    [Required(ErrorMessage = "Не указана фамилия")]
    public string LastName { get; set; } = null!;
    
    [Display(Name = "Область")]
    public string? Area { get; set; }
    
    [Display(Name = "Город")]
    public string? City { get; set; }
    
    [Display(Name = "Почтовый индекс")]
    public string? IndexCode { get; set; }
    
    [Display(Name = "Адрес")]
    public string? Address { get; set; }
    
    [Display(Name = "Номер телефона")]
    public string? Phone { get; set; }
    
    [Display(Name = "Электронная почта")]
    [Required(ErrorMessage = "Не указана электронная почта")]
    public string Email { get; set; } = null!;
}