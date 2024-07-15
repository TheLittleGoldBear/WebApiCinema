using AutoMapper;
using Cinema.Dto;
using Cinema.Model;

namespace Cinema.Helper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Film, FilmDto>()
               .ForMember(dest => dest.DirectorDto,
               opt => opt.MapFrom(
                   src => src.Director));

            CreateMap<FilmCreateDto, Film>();

            CreateMap<FilmDto, Film>()
                .ForMember(dest => dest.Director,
                opt => opt.MapFrom(
                    src => src.DirectorDto));

            CreateMap<Room, RoomDto>();
            CreateMap<RoomDto, Room>();
            
            CreateMap<Director, DirectorDto>();
            CreateMap<DirectorDto, Director>();
        }
    }
}
