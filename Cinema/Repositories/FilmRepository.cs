using Cinema.Data;
using Cinema.Interfaces;
using Cinema.Model;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Repositories
{
    public class FilmRepository(AppDbContex contex) : IFilmRepository
    {
        private readonly AppDbContex _contex = contex;

        public bool FilmExists(int filmId)
        {
            return _contex.Films.Any(x => x.Id == filmId);
        }

        public Film GetFilmById(int filmId)
        {
            return _contex.Films.FirstOrDefault(x => x.Id == filmId);
        }
        public ICollection<Film> GetFilms()
        {
            return _contex.Films.Include(x => x.Director).ToList();
        }

        public ICollection<Film> GetFilmByDirector(int directorId)
        {
            return _contex.Films
                .Where(x => x.Director.Id == directorId)
                .Include(x => x.Director)
                .ToList();
        }

        public bool CreateFilm(Film film)
        {
            //spr czy jest reżyser

            _contex.Films.Add(film);
            return Saved();
        }

        public bool UpdateFilm(Film film)
        {
            _contex.Films.Update(film);
            return Saved();
        }
        
        public bool DeleteFilm(Film film)
        {
            _contex.Films.Remove(film);
            return Saved();
        }
        public bool AddFilmRoomForFilm(Film film, FilmRoom filmRoom)
        {
            if(film == null || filmRoom == null)
            {
                return false;
            }

            if(film.FilmRooms == null)
            {
                film.FilmRooms = new();
            }

            film.FilmRooms.Add(filmRoom);

            return Saved();
        }

        public bool Saved()
        {
            var saved =_contex.SaveChanges();
            return saved > 0;
        }

    }
}
