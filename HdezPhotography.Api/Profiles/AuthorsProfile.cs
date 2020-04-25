using AutoMapper;
using HdezPhotography.Api.Entities;
using HdezPhotography.Api.Helpers;
using HdezPhotography.Api.Models;

namespace HdezPhotography.Api.Profiles {
    public class AuthorsProfile : Profile {
        public AuthorsProfile() {
            CreateMap<Author, AuthorDto>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")
                )
                .ForMember(
                    dest => dest.Age,
                    opt => opt.MapFrom(src => src.DateOfBirth.GetCurrentAge())
                );
        }
    }
}
