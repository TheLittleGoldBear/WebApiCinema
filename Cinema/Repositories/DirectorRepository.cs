using Cinema.Data;
using Cinema.Interfaces;
using Cinema.Model;

namespace Cinema.Repositories
{
    public class DirectorRepository(AppDbContex contex) : IDirectorRepository
    {
        private readonly AppDbContex _contex = contex;

        public bool DirectorExist(int directorId)
        {
            return _contex.Directors.Any(x => x.Id == directorId);
        }

        public Director GetDirector(int directorId)
        {
            return _contex.Directors.FirstOrDefault(x => x.Id == directorId);
        }

        public ICollection<Director> GetDirectors()
        {
            return _contex.Directors.ToList();
        }

        public bool CreateDirector(Director director)
        {
            _contex.Directors.Add(director);
            return Saved();
        }
        public bool AddFilmToDirector(Director director, Film film)
        {
            if (director == null || film == null)
            {
                return false;
            }

            if (director.Films == null)
            {
                director.Films = new List<Film>();
            }

            director.Films.Add(film);

            return Saved();
        }

        public bool Saved()
        {
            var saved = _contex.SaveChanges();
            return saved > 0;
        }

    }
}
