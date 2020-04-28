using AutoMapper;
using HdezPhotography.Api.Entities;
using HdezPhotography.Api.Models;

namespace HdezPhotography.Api.Profiles {
    public class PhotosProfile : Profile {
        public PhotosProfile() {
            CreateMap<Photo, PhotoDto>();
        }
    }
}
