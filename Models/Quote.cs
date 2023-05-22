using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork.Models
{
    public class Quote
    {
        public int Id { get; set; }
        [Display(Name = "Книга")]
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        [Display(Name = "Книга")]
        public Book Book { get; set; }
        [Display(Name = "Текст")]
        public string Text { get; set; }
    }
}