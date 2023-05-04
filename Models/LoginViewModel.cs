using System.ComponentModel.DataAnnotations;

namespace CourseWork.Models
{
    public class LoginViewModel
    {
        [Required (ErrorMessage = "Не указано адрес электронной почты")]
        [EmailAddress]
        public string? Email {get; set;}
        [Required (ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string? Password {get; set;}
    }
}