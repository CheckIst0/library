using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Путь к картинке")]
        public string ImageSource { get; set; }
        [Display(Name = "Автор")]
        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public Author Author { get; set; }
        [Display(Name = "Издательство")]
        public int PublisherId { get; set; }
        [ForeignKey("PublisherId")]
        [Display(Name = "Издательство")]
        public Publisher Publisher { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Display(Name = "Количество")]
        public int Quantity { get; set; }
        [Display(Name = "Форма")]
        public int FormId { get; set; }
        [ForeignKey("FormId")]
        [Display(Name = "Форма")]
        public Form Form { get; set; }
        [Display(Name = "Жанр")]
        public int GenreId { get; set; }
        [ForeignKey("GenreId")]
        [Display(Name = "Жанр")]
        public Genre Genre { get; set; }
        [Display(Name = "Направление")]
        public int StyleId { get; set; }
        [Display(Name = "Направление")]
        [ForeignKey("StyleId")]
        public Style Style { get; set; }
        public List<Quote> Quotes { get; set; }
    }
}
