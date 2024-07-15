using Cinema.Model;

namespace Cinema.Interfaces
{
    public interface IRoomRepository
    {
        Room GetRoom(int roomId);
        bool RoomExist(int roomId);
        ICollection<Room> GetRooms();
        bool CreateRoom(Room room);
        bool UpdateRoom(Room room);
        bool DeleteRoom(Room room);
        bool AddFilmRoomForRoom(Room room, FilmRoom filmRoom);
        bool Saved();
    }
}
