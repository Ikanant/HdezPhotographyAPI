using AutoMapper;
using HdezPhotography.Api.Helpers;
using HdezPhotography.Api.Models;
using HdezPhotography.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace HdezPhotography.Api.Controllers {

    [ApiController]
    [Route("api/members")]
    public class MembersController : ControllerBase {

        private readonly IPhotoLibraryRepository _photoLibraryRepository;
        private readonly IMapper _mapper;

        public MembersController (IPhotoLibraryRepository photoLibraryRepository, IMapper mapper) {
            _photoLibraryRepository = photoLibraryRepository ?? throw new ArgumentNullException(nameof(photoLibraryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<IEnumerable<MemberDto>> GetMembers() {
            var membersFromRepo = _photoLibraryRepository.GetMembers();

            return Ok(_mapper.Map<IEnumerable<MemberDto>>(membersFromRepo));
        }

        [HttpGet("{memberId:int}")]
        public IActionResult GetMember(int memberId) {
            var memberFromRepo = _photoLibraryRepository.GetMember(memberId);

            if (memberFromRepo == null) {
                return NotFound();
            }

            return Ok(_mapper.Map<MemberDto>(memberFromRepo));
        }
    }
}
