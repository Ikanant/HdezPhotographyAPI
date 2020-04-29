using AutoMapper;
using HdezPhotography.Api.Entities;
using HdezPhotography.Api.Helpers;
using HdezPhotography.Api.Models;
using HdezPhotography.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public ActionResult<MemberDto> CreateMember(MemberImportDto newMember) {
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

        [HttpGet("({ids})", Name = "GetMemberCollection")]
        public ActionResult<MemberDto> GetMemberCollection(
            [FromRoute]
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<int> ids) {

            if (ids == null) {
                return BadRequest();
            }
            var memberEntities = _photoLibraryRepository.GetMembers(ids);

            // Extra check top see if the count of IDs matches the Count of Entities... if they are different , something is wrong
            if (ids.Count() != memberEntities.Count()) {
                return NotFound();
            }

            var membersToReturn = _mapper.Map<IEnumerable<MemberDto>>(memberEntities);
            
            return Ok(membersToReturn);
        }

        [HttpPost("CreateMemberCollection")]
        public ActionResult<MemberDto> CreateMemberCollection(IEnumerable<MemberImportDto> newMembers) {
            if (newMembers == null) {
                return BadRequest(); // <--- Not necessary
            }
            var memberEntities = _mapper.Map<IEnumerable<Member>>(newMembers);

            foreach (var entitty in memberEntities) {
                _photoLibraryRepository.AddMember(entitty);
            }
            _photoLibraryRepository.Save();

            // Get the newly created entities
            var newMemberEntities = _mapper.Map<IEnumerable<MemberDto>>(memberEntities);

            // The new IDs of all the new entities... this is they key
            var idsAsString = string.Join(",", newMemberEntities.Select(s => s.ID));

            return CreatedAtRoute(
                "GetMemberCollection",
                new { ids = idsAsString },
                newMemberEntities
            );
        }
    }
}
