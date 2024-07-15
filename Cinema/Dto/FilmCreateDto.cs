using Cinema.Model;

namespace Cinema.Dto
{
    public class FilmCreateDto
    {
        public string Name { get; set; }
        public List<int> RoomIds { get; set; }
    }
}
