using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork.Models
{
    public class IssueHistory
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public Book Book { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        [Column(TypeName = "Date")]
        public DateTime IssueDate { get; set; }
        [Column(TypeName = "Date")]
        public DateTime EstimatedReturnDate { get; set; }
        [Column(TypeName = "Date")]
        public DateTime FactReturnDate { get; set; }
    }
}
