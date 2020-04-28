using AutoMapper;
using HdezPhotography.Api.Entities;
using HdezPhotography.Api.Models;

namespace HdezPhotography.Api.Profiles {
    public class MembersProfile : Profile {
        public MembersProfile() {
            CreateMap<Member, MemberDto>();
        }
    }
}
