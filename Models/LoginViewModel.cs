using System.ComponentModel.DataAnnotations;

namespace CourseWork.Models
{
    public class LoginViewModel
    {
        [Required (ErrorMessage = "Не указан адрес электронной почты")]
        [EmailAddress(ErrorMessage = "Некорректный адрес электронной почты")]
        public string? Email {get; set;}
        [Required (ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string? Password {get; set;}
    }
}