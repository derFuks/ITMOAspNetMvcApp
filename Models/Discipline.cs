namespace ITMOAspNetMvcApp.Models
{
    public class Discipline
    {
        public int DisciplineId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}
