using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Display(Name="Название")]
        public string Name { get; set; }
        [DataType(DataType.ImageUrl)]
        [Display(Name="Путь к картинке")]
        public string ImageSource { get; set; }
        [Display(Name="Автор")]
        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public Author Author { get; set; }
        [Display(Name="Описание")]
        public string Description { get; set; }
        [Display(Name="Количество")]
        public int Quantity { get; set; }
        [Display(Name="Форма")]
        public int FormId { get; set; }
        [ForeignKey("FormId")]
        public Form Form { get; set; }
        [Display(Name="Род")]
        public int GenusId { get; set; }
        [ForeignKey("GenusId")]
        public Genus Genus { get; set; }
        [Display(Name="Содержание")]
        public int ContentId { get; set; }
        [ForeignKey("ContentId")]
        public Content Content { get; set; }
        [Display(Name="Направление")]
        public int StyleId { get; set; }
        [ForeignKey("StyleId")]
        public Style Style { get; set; }
    }
}
