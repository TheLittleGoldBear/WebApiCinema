using Cinema.Model;

namespace Cinema.Interfaces
{
    public interface IFilmRepository
    {
        bool FilmExists(int filmId);
        Film GetFilmById(int filmId);
        ICollection<Film> GetFilms();
        ICollection<Film> GetFilmByDirector(int directorId);
        bool CreateFilm(Film film);
        bool UpdateFilm(Film film);
        bool DeleteFilm(Film film);
        bool AddFilmRoomForFilm(Film film, FilmRoom filmRoom);
        bool Saved();
    }
}
