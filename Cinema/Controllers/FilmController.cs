using AutoMapper;
using Cinema.Data;
using Cinema.Dto;
using Cinema.Interfaces;
using Cinema.Model;
using Cinema.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController(
        AppDbContex contex,
        IMapper mapper, 
        IFilmRepository filmRepository,
        IRoomRepository roomRepository,
        IDirectorRepository directorRepository
        ) : Controller
    {
        private readonly AppDbContex _contex = contex;
        private readonly IMapper _mapper = mapper;
        private readonly IFilmRepository _filmRepository = filmRepository;
        private readonly IRoomRepository _roomRepository = roomRepository;
        private readonly IDirectorRepository _directorRepository = directorRepository;

        [HttpGet("GetFilms")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FilmDto>))]
        public IActionResult GetFilms() {

           var films = _mapper.Map<List<Film>, List<FilmDto>>(_filmRepository.GetFilms().ToList());

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(films);
        }
        
        [HttpGet("GetFilm/{filmId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FilmDto>))]
        public IActionResult GetFilm(int filmId) {
            if(!_filmRepository.FilmExists(filmId))
            {
                return NotFound();
            }

            var film = _mapper.Map<Film, FilmDto>(_filmRepository.GetFilmById(filmId));

            if(!ModelState.IsValid)
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

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(film);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult CrateFilm(
            [FromQuery] int directorId,
            [FromQuery] int roomId,
            [FromBody]FilmCreateDto filmDto) 
        {
            if(filmDto == null)
            {
                return BadRequest(ModelState);
            }

            if(!_directorRepository.DirectorExist(directorId) || !_roomRepository.RoomExist(roomId))
            {
                return NotFound(directorId);
            }

            var director = _directorRepository.GetDirector(directorId);
            var room = _roomRepository.GetRoom(roomId);

            var film = _mapper.Map<Film>(filmDto);

            film.Director = director;

            /* var filmRoom = new FilmRoom() { Film = film, Room = room };
             film.FilmRooms = new();
             film.FilmRooms.Add(filmRoom);

             room.FilmRooms = new();
             room.FilmRooms.Add(filmRoom);*/


            /*if (filmDto.RoomId != null)
            {
                foreach (var room in filmDto.RoomId)
                {
                    if (!_roomRepository.RoomExist(room))
                    {
                        return BadRequest(ModelState);
                    }

                    var existingRoom = _roomRepository.GetRoom(room);
                    if (existingRoom != null)
                    {
                        film.FilmRooms.Add(new FilmRoom { Room = existingRoom, Film = film });
                    }
                }
            }

            if (filmDto.Rooms != null)
            {
                foreach (var room in filmDto.Rooms)
                {
                    var mappedRoom = _mapper.Map<Room>(room);
                    _roomRepository.CreateRoom(mappedRoom);
                    film.FilmRooms.Add(new FilmRoom() { Room = mappedRoom, Film = film });
                }
            }*/

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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
        public IActionResult UpdateFilme([FromBody]FilmCreateDto filmDto) 
        {
            if(filmDto == null)
            {
                return BadRequest(ModelState);
            }

            var film = _mapper.Map<FilmCreateDto, Film>(filmDto);

            


            if(!ModelState.IsValid)
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
            if(!_filmRepository.FilmExists(filmId))
            {
                return BadRequest(ModelState);
            }

            var film = _filmRepository.GetFilmById(filmId);

            if(!ModelState.IsValid)
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
