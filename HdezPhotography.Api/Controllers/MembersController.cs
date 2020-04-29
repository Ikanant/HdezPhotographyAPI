using AutoMapper;
using HdezPhotography.Api.Entities;
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

        [HttpGet("{memberId:int}", Name = "GetMember")]
        public IActionResult GetMember(int memberId) {
            var memberFromRepo = _photoLibraryRepository.GetMember(memberId);

            if (memberFromRepo == null) {
                return NotFound();
            }

            return Ok(_mapper.Map<MemberDto>(memberFromRepo));
        }

        [HttpPost]
        public ActionResult<PhotoDto> CreateMember(MemberImportDto newMember) {
            if (newMember == null) {
                return BadRequest(); // <--- Not necessary
            }
            var memberEntity = _mapper.Map<Member>(newMember);
            _photoLibraryRepository.AddMember(memberEntity);
            _photoLibraryRepository.Save();

            // Map the member entity (with now an ID filled up) to a MemberDto
            // We RETURN the new MemberDTO after action has been completed
            var memberToReturn = _mapper.Map<MemberDto>(memberEntity);

            return CreatedAtRoute("GetMember", new { memberID = memberEntity.ID }, memberToReturn);
        }
    }
}
