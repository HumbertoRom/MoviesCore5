using AutoMapper;
using Movies.Models;

namespace Movies.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Gender, GenderDto>().ReverseMap();
            CreateMap<Gender, CreateGenderDto>().ReverseMap();
            CreateMap<Actor, ActorDto>().ReverseMap();
            CreateMap<CreateActorDto, Actor>().ReverseMap()
                .ForMember(x => x.Photo, opts => opts.Ignore());
        }
    }
}
