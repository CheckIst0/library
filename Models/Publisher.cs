using System.ComponentModel.DataAnnotations;

namespace CourseWork.Models
{

    public class Publisher
    {
        public int Id { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Display(Name = "Адрес")]
        public string Address { get; set; }
        [Display(Name = "Книги")]
        public List<Book> Books { get; set; }
    }
}