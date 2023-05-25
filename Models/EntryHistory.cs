using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork.Models
{
    public class EntryHistory
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int UserId {get; set;}
        [ForeignKey("UserId")]
        public User User {get; set;}
        public DateTime EntryDate { get; set; }
    }
}