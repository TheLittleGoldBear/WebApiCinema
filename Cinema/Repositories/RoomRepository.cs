using Cinema.Data;
using Cinema.Interfaces;
using Cinema.Model;

namespace Cinema.Repositories
{
    public class RoomRepository(AppDbContex contex) : IRoomRepository
    {
        private readonly AppDbContex _contex = contex;

        public Room GetRoom(int roomId)
        {
            return _contex.Rooms.FirstOrDefault(x => x.Id == roomId);
        }

        public bool RoomExist(int roomId)
        {
            return _contex.Rooms.Any(x => x.Id == roomId);
        }
        public ICollection<Room> GetRooms()
        {
            return _contex.Rooms.ToList();
        }

        public bool CreateRoom(Room room)
        {
            _contex.Rooms.Add(room);
            return Saved();
        }

        public bool UpdateRoom(Room room)
        {
            _contex.Rooms.Update(room);
            return Saved();
        }

        public bool DeleteRoom(Room room)
        {
            _contex.Rooms.Remove(room);
            return Saved();
        }

        public bool Saved()
        {
            var saved = _contex.SaveChanges();
            return saved > 0;
        }
    }
}
