using System.ComponentModel.DataAnnotations;

namespace CourseWork.Models
{
    public class Author
    {
        public int Id { get; set; }
        [Display(Name = "Имя автора")]
        public string Name { get; set; }
        [Display(Name = "Биография")]
        public string Biography { get; set; }
        [Display(Name = "Книги автора")]
        public List<Book> Books { get; set; }
        public List<Award> Awards { get; set; }
    }
}
