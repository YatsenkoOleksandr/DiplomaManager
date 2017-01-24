using System.ComponentModel.DataAnnotations;

namespace DiplomaManager.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Не указан логин.")]
        [MaxLength(50)]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан пароль.")]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}
