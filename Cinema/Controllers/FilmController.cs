using AutoMapper;
using Cinema.Data;
using Cinema.Dto;
using Cinema.Interfaces;
using Cinema.Model;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController(
        IMapper mapper,
        IFilmRepository filmRepository,
        IRoomRepository roomRepository,
        IDirectorRepository directorRepository
        ) : Controller
    {
        private readonly IMapper _mapper = mapper;
        private readonly IFilmRepository _filmRepository = filmRepository;
        private readonly IRoomRepository _roomRepository = roomRepository;
        private readonly IDirectorRepository _directorRepository = directorRepository;

        [HttpGet("GetFilms")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FilmDto>))]
        public IActionResult GetFilms()
        {

            var films = _mapper.Map<List<Film>, List<FilmDto>>(_filmRepository.GetFilms().ToList());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(films);
        }

        [HttpGet("GetFilm/{filmId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FilmDto>))]
        public IActionResult GetFilm(int filmId)
        {
            if (!_filmRepository.FilmExists(filmId))
            {
                return NotFound();
            }

            var film = _mapper.Map<Film, FilmDto>(_filmRepository.GetFilmById(filmId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(film);
        }

        [HttpGet("GetFilmByDirector/{directorId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FilmDto>))]
        public IActionResult GetFilmByDirector(int directorId)
        {
            var film = _mapper.Map<List<Film>, List<FilmDto>>(_filmRepository.GetFilmByDirector(directorId).ToList());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(film);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult CrateFilm(
            [FromQuery] int directorId,
            [FromBody] FilmCreateDto filmDto)
        {
            if (filmDto == null)
            {
                return BadRequest(ModelState);
            }

            if (filmDto.RoomIds == null)

                if (!_directorRepository.DirectorExist(directorId))
                {
                    return NotFound(directorId);
                }

            var film = _mapper.Map<Film>(filmDto);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var director = _directorRepository.GetDirector(directorId);
            film.Director = director;

            foreach (var roomId in filmDto.RoomIds)
            {
                if (_roomRepository.RoomExist(roomId))
                {
                    var room = _roomRepository.GetRoom(roomId);
                    var filmRoom = new FilmRoom() { Film = film, Room = room };

                    _filmRepository.AddFilmRoomForFilm(film, filmRoom);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }


            if (!_filmRepository.CreateFilm(film))
            {
                ModelState.AddModelError("", "Unable to create");

                return StatusCode(500, ModelState);
            }

            return Ok("Created");
        }


        [HttpPut]
        [ProducesResponseType(200)]
        public IActionResult UpdateFilme([FromBody] FilmCreateDto filmDto)
        {
            if (filmDto == null)
            {
                return BadRequest(ModelState);
            }

            var film = _mapper.Map<FilmCreateDto, Film>(filmDto);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_filmRepository.UpdateFilm(film))
            {
                ModelState.AddModelError("", "Unable to update");

                return StatusCode(500, ModelState);
            }

            return Ok("Updated");
        }

        [HttpDelete("{filmId}")]
        [ProducesResponseType(200)]
        public IActionResult Delete(int filmId)
        {
            if (!_filmRepository.FilmExists(filmId))
            {
                return BadRequest(ModelState);
            }

            var film = _filmRepository.GetFilmById(filmId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_filmRepository.DeleteFilm(film))
            {
                ModelState.AddModelError("", "Unable to delete");

                return StatusCode(500, ModelState);
            }

            return Ok("Sucessfully deleted");
        }
    }
}
