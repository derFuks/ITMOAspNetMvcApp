using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ITMOAspNetMvcApp.Models
{
    // Указываем, что сущность не имеет ключа
    [Keyless]
    // Маппим на представление с именем "Табель посещения"
    [Table("Табель посещения")]
    public class AttendanceRecord
    {
        public int student_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        
        // Поле из представления с алиасом "Всего посетил занятий"
        [Column("Всего посетил занятий")]
        public int TotalAttendanceScore { get; set; }
        
        // Поле из представления: оценка по экзамену (если равна 0, отображается как "без оценки")
        [Column("Оценка по экзамену")]
        public string ExamScore { get; set; }
        
        // для устранения косяка в C# даты меняем на вид DYYYYMMDD и маппим их на соответствующие названия столбцов.
        
        [Column("17.03.2025")]
        public int D17032025 { get; set; }
        
        [Column("18.03.2025")]
        public int D18032025 { get; set; }
        
        [Column("20.03.2025")]
        public int D20032025 { get; set; }
        
        [Column("24.03.2025")]
        public int D24032025 { get; set; }
        
        [Column("25.03.2025")]
        public int D25032025 { get; set; }
        
        [Column("28.03.2025")]
        public int D28032025 { get; set; }
        
        [Column("31.03.2025")]
        public int D31032025 { get; set; }
        
        [Column("01.04.2025")]
        public int D01042025 { get; set; }
        
        [Column("03.04.2025")]
        public int D03042025 { get; set; }
        
        [Column("07.04.2025")]
        public int D07042025 { get; set; }
    }
}
