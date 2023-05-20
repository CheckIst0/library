using System.ComponentModel.DataAnnotations;

namespace CourseWork.Models
{
    public class Role
    {
        public int Id { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }
    }
}