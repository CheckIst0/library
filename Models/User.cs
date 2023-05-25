using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId {get; set;}
        [ForeignKey("RoleId")]
        public Role Role { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Patronymic { get; set; }
        public char Gender { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
    }
}