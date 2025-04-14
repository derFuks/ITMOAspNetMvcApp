using System;
using System.Collections.Generic;

namespace ITMOAspNetMvcApp.Models
{
    public class Lesson
    {
        public int LessonId { get; set; }
        public int DisciplineId { get; set; }
        public DateTime LessonDate { get; set; }
        
        public Discipline Discipline { get; set; }
        public ICollection<Attendance>? Attendances { get; set; }
    }
}
