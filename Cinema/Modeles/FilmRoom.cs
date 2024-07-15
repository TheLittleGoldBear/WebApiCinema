namespace Cinema.Model
{
    public class FilmRoom
    {
        public int FilmId { get; set; }
        public int RoomId { get; set; }

        public Film Film { get; set; }
        public Room Room { get; set; }
    }
}
