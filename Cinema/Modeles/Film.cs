using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Model
{
    public class Film
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DirectorId { get; set; }
        [ForeignKey(nameof(DirectorId))]
        public Director Director { get; set; }
        public List<FilmRoom> FilmRooms { get; set; }
    }
}
