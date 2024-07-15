namespace Cinema.Model
{
    public class Room
    {
        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public List<FilmRoom> FilmRooms { get; set; }
    }
}
