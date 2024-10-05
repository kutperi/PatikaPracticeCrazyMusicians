using System.Diagnostics.Eventing.Reader;

namespace PatikaPracticeCrazyMusicians.Models
{
    public class Musician
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Job { get; set; }
        public string FunFact { get; set; }
        public bool IsDeleted { get; set; }
    }
}
