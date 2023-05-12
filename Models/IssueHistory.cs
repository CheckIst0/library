using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork.Models
{
    public class IssueHistory
    {
        public int Id { get; set; }
        [Display(Name="Книга")]
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        [Display(Name="Книга")]
        public Book Book { get; set; }
        [Display(Name="Пользователь")]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        [Display(Name="Пользователь")]
        public User User { get; set; }
        [Column(TypeName = "Date")]
        [Display(Name="Дата выдачи")]
        public DateTime IssueDate { get; set; }
        [Column(TypeName = "Date")]
        [Display(Name="Предполагаемая дата возвращения")]
        public DateTime EstimatedReturnDate { get; set; }
        [Column(TypeName = "Date")]
        [Display(Name="Фактическая дата возвращения")]
        public DateTime? FactReturnDate { get; set; }
    }
}
