using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork.Models
{
    public class Award
    {
        public int Id { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Display(Name = "Автор")]
        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        [Display(Name = "Автор")]
        public Author Author { get; set; }

        [Column(TypeName = "Date")]
        [Display(Name = "Дата получения")]
        public DateTime ReceiptDate { get; set; }
    }
}
