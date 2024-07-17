using Cinema.Data;
using Cinema.Model;

namespace Cinema
{
    public class Seed(AppDbContex context)
    {
        private readonly AppDbContex _context = context;

        public void SeedDataContext()
        {
            if (!_context.FilmRooms.Any())
            {
                var directors = new List<Director>
                {
                    new Director { FullName = "Steven Spielberg" },
                    new Director { FullName = "Christopher Nolan" },
                    new Director { FullName = "Martin Scorsese" }
                };

                var films = new List<Film>
                {
                    new Film { Name = "Star Wars", Director = directors[0] },
                    new Film { Name = "Lost Highway", Director = directors[1] },
                    new Film { Name = "Mullholland Drive", Director = directors[2] }
                };

                var rooms = new List<Room>
                {
                    new Room { RoomNumber = 10},
                    new Room { RoomNumber = 6},
                    new Room { RoomNumber = 4}
                };

                var filmRooms = new List<FilmRoom>
                {
                    new FilmRoom { Film = films[0], Room = rooms[0] },
                    new FilmRoom { Film = films[1], Room = rooms[1] },
                    new FilmRoom { Film = films[2], Room = rooms[2] },
                    new FilmRoom { Film = films[0], Room = rooms[2] },
                    new FilmRoom { Film = films[1], Room = rooms[0] },
                };

                _context.FilmRooms.AddRange(filmRooms);
                _context.SaveChanges();
            }
        }
    }
}