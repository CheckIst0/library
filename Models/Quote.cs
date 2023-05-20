using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork.Models
{
    public class Quote
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public Book Book { get; set; }
        public string Text { get; set; }
    }
}