using System.ComponentModel.DataAnnotations;

namespace JwtDemo.Models
{
    public class StudentDetails
    {


        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Class { get; set; }
        public string Password { get; set; }
    }
}