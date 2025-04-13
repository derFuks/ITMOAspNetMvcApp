namespace ITMOAspNetMvcApp.Models
{
    public class Attendance
    {
        public int AttendanceId { get; set; }
        
        // Внешние ключи
        public int StudentId { get; set; }
        public int LessonId { get; set; }
        
        public bool WasPresent { get; set; }
        
        // Навигационные свойства
        public Student Student { get; set; }
        public Lesson Lesson { get; set; }
    }
}
