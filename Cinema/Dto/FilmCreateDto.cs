using Cinema.Model;

namespace Cinema.Dto
{
    public class FilmCreateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        /*public DirectorDto DirectorDto { get; set; }
         public int? DirectorId {  get; set; }
         public List<int> RoomId { get; set; }
         public List<RoomDto> Rooms { get; set; }*/
    }
}
