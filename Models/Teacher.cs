using System.Collections.Generic;

namespace ITMOAspNetMvcApp.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<Discipline>? Disciplines { get; set; }
    }
}
