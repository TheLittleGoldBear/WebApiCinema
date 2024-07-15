using Cinema.Model;

namespace Cinema.Interfaces
{
    public interface IDirectorRepository
    {
        bool DirectorExist(int directorId);
        Director GetDirector(int directorId);
        ICollection<Director> GetDirectors();
        bool AddFilmToDirector(Director director, Film film);
        bool CreateDirector(Director director);
        bool Saved();
    }
}
