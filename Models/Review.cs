using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Rating {get; set;}
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public Book Book { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}