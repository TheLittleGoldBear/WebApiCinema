using AutoMapper;
using Cinema.Dto;
using Cinema.Model;

namespace Cinema.Helper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Room, RoomDto>();
            CreateMap<RoomDto, Room>();

            CreateMap<Film, FilmDto>()
               .ForMember(dest => dest.DirectorDto,
               opt => opt.MapFrom(
                   src => src.Director))
               .ForMember(dest => dest.RoomDtos,
               opt => opt.MapFrom(
                   src => src.FilmRooms.Select(x => x.Room)));

            CreateMap<FilmCreateDto, Film>();

            CreateMap<FilmDto, Film>()
                .ForMember(dest => dest.Director,
                opt => opt.MapFrom(
                    src => src.DirectorDto));
            
            CreateMap<Director, DirectorDto>();
            CreateMap<DirectorDto, Director>();
        }
    }
}
