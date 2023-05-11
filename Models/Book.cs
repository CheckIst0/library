using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.ImageUrl)]
        public string ImageSource { get; set; }
        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public Author Author { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int FormId { get; set; }
        [ForeignKey("FormId")]
        public Form Form { get; set; }
        public int GenusId { get; set; }
        [ForeignKey("GenusId")]
        public Genus Genus { get; set; }
        public int ContentId { get; set; }
        [ForeignKey("ContentId")]
        public Content Content { get; set; }
        public int StyleId { get; set; }
        [ForeignKey("StyleId")]
        public Style Style { get; set; }
    }
}
