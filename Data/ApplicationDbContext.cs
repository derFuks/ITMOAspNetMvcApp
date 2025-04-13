using ITMOAspNetMvcApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ITMOAspNetMvcApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
        
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<AttendanceRecord> AttendanceRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Имена таблиц в соответствие с реальными в БД.
            modelBuilder.Entity<Student>().ToTable("students");
            modelBuilder.Entity<Teacher>().ToTable("teachers");
            modelBuilder.Entity<Discipline>().ToTable("disciplines");
            modelBuilder.Entity<Lesson>().ToTable("lessons");
            modelBuilder.Entity<Attendance>().ToTable("attendance");

            // Задаём уникальное ограничение для Attendance
            modelBuilder.Entity<Attendance>()
                .HasIndex(a => new { a.StudentId, a.LessonId })
                .IsUnique()
                .HasDatabaseName("unique_attendance");

            modelBuilder.Entity<AttendanceRecord>()
                .HasNoKey()
                .ToView("Табель посещения");
            
            // Приведение названий столбцов
            //Student
            modelBuilder.Entity<Student>().Property(s => s.StudentId).HasColumnName("student_id");
            modelBuilder.Entity<Student>().Property(s => s.FirstName).HasColumnName("first_name");
            modelBuilder.Entity<Student>().Property(s => s.LastName).HasColumnName("last_name");
            modelBuilder.Entity<Student>().Property(s => s.Email).HasColumnName("email");
            modelBuilder.Entity<Student>().Property(s => s.TotalAttendanceScore).HasColumnName("total_attendance_score");
            modelBuilder.Entity<Student>().Property(s => s.ExamScore).HasColumnName("exam_score");
            
            //Lesson
            modelBuilder.Entity<Lesson>().Property(s => s.LessonId).HasColumnName("lesson_id");
            modelBuilder.Entity<Lesson>().Property(s => s.DisciplineId).HasColumnName("discipline_id");
            modelBuilder.Entity<Lesson>().Property(s => s.LessonDate).HasColumnName("lesson_date");

            //Attendance
            modelBuilder.Entity<Attendance>().Property(s => s.AttendanceId).HasColumnName("attendance_id");
            modelBuilder.Entity<Attendance>().Property(s => s.StudentId).HasColumnName("student_id");
            modelBuilder.Entity<Attendance>().Property(s => s.LessonId).HasColumnName("lesson_id");
            modelBuilder.Entity<Attendance>().Property(s => s.WasPresent).HasColumnName("was_present");

            //Teachers
            modelBuilder.Entity<Teacher>().Property(s => s.TeacherId).HasColumnName("teacher_id");
            modelBuilder.Entity<Teacher>().Property(s => s.FirstName).HasColumnName("first_name");
            modelBuilder.Entity<Teacher>().Property(s => s.LastName).HasColumnName("last_name");
            modelBuilder.Entity<Teacher>().Property(s => s.Email).HasColumnName("email");

            //Disciplines
            modelBuilder.Entity<Discipline>().Property(s => s.DisciplineId).HasColumnName("discipline_id");
            modelBuilder.Entity<Discipline>().Property(s => s.Name).HasColumnName("name");
            modelBuilder.Entity<Discipline>().Property(s => s.Description).HasColumnName("description");
            modelBuilder.Entity<Discipline>().Property(s => s.TeacherId).HasColumnName("teacher_id");

            base.OnModelCreating(modelBuilder);
        }
    }
}
