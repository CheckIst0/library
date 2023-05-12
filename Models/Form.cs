using System.ComponentModel.DataAnnotations;

namespace CourseWork.Models
{
    public class Form
    {
        public int Id { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
    }
}
