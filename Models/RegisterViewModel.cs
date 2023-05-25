using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork.Models
{
    public class RegisterViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите адрес электронной почты")]
        [EmailAddress(ErrorMessage = "Некорректный адрес электронной почты")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите пароль")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Длина пароля должна быть от 8 до 50 символов")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])[a-zA-Z0-9]{8,50}$",
        ErrorMessage = "Пароль должен содержать символы латинского алфавита в верхнем и нижнем регистре и цифры, недопустимо вводить спецсимволы")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите фамилию")]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите отчество")]
        public string Patronymic { get; set; }
        [Required(ErrorMessage = "Пожалуйста, выберите пол")]
        public char Gender { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите адрес")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите возраст")]
        [Range(14, 120, ErrorMessage = "Недопустимый возраст")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите номер телефона")]
        [Phone(ErrorMessage = "Некорректный номер телефона")]
        public string Phone { get; set; }
    }
}