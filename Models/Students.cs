using System.Collections.Generic;

namespace ITMOAspNetMvcApp.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int TotalAttendanceScore { get; set; }
        public int ExamScore { get; set; }
        
        // Навигационное свойство для посещаемости
        public ICollection<Attendance>? Attendances { get; set; }
    }
}
