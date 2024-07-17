using Cinema.Model;

namespace Cinema.Dto
{
    public class FilmDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DirectorDto DirectorDto { get; set; }
        public List<RoomDto> RoomDtos { get; set; }
    }
}
