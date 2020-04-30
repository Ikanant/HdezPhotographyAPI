using AutoMapper;
using HdezPhotography.Api.Entities;
using HdezPhotography.Api.Helpers;
using HdezPhotography.Api.Models;
using HdezPhotography.Api.ResourceParameters;
using HdezPhotography.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace HdezPhotography.Api.Controllers {

    [ApiController]
    [Route("api/members/{memberID}/photos")]
    public class PhotosController : ControllerBase {

        private readonly IPhotoLibraryRepository _photoLibraryRepository;
        private readonly IMapper _mapper;

        public PhotosController (IPhotoLibraryRepository photoLibraryRepository, IMapper mapper) {
            _photoLibraryRepository = photoLibraryRepository ?? throw new ArgumentNullException(nameof(photoLibraryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpOptions]
        public IActionResult GetMembersOptions() {
            Response.Headers.Add("Allow", "GET, POST, OPTIONS");
            return Ok();
        }

        [HttpGet]
        public ActionResult<IEnumerable<PhotoDto>> GetPhotosForMember(int memberID, [FromQuery] PhotosResourceParameters photosResourceParameters) {
            if (!_photoLibraryRepository.MemberExists(memberID)) {
                return NotFound();
            }

            var photosFromRepo = _photoLibraryRepository.GetPhotos(memberID, photosResourceParameters);
            return Ok(_mapper.Map<IEnumerable<PhotoDto>>(photosFromRepo));
        }

        [HttpGet("{photoID:int}", Name="GetPhotoForMember")]
        public ActionResult<PhotoDto> GetPhotoForMember(int memberID, int photoID) {
            if (!_photoLibraryRepository.MemberExists(memberID)) {
                return NotFound();
            }

            var photoFromRepo = _photoLibraryRepository.GetPhoto(memberID, photoID);
            
            if (photoFromRepo == null) {
                return NotFound();
            }

            return Ok(_mapper.Map<PhotoDto>(photoFromRepo));
        }

        [HttpPost]
        public ActionResult<PhotoDto> CreatePhotoForMember(int memberID, PhotoImportDto newPhoto) {
            // In old DotNet we would have had to check if new object was valid to be serialized into a Photo object. We no longer need to include that...
            // APIController attribute is taking care of this
            if (newPhoto == null) {
                return BadRequest(); // <--- Not necessary
            }

            var photoEntity = _mapper.Map<Photo>(newPhoto);
            _photoLibraryRepository.AddPhoto(memberID, photoEntity);
            _photoLibraryRepository.Save();

            // Map the photo entity (with now an ID filled up) to a PhotoDto
            // We RETURN the new PhotoDTO after action has been completed
            var photoToReturn = _mapper.Map<PhotoDto>(photoEntity);

            return CreatedAtRoute("GetPhotoForMember", new {
                memberID = memberID,
                photoID = photoEntity.ID
            },
            photoToReturn
            );
        }

        [HttpPut("{courseID}")]
        public ActionResult UpdatePhotoForMember(int memberID, int photoID, PhotoUpdateDto photoToUpdate) {
            if (!_photoLibraryRepository.MemberExists(memberID)) {
                return NotFound();
            }

            var photoForMemberFromRepo = _photoLibraryRepository.GetPhoto(memberID, photoID);

            if (photoForMemberFromRepo == null) {
                return NotFound();
            }

            // Order of events:
            // Map the entity to a photoUpdateDto
            // Apply changes to the Dto
            // Map the Dto back to an entity.... Automapper will take care of this for us...
            _mapper.Map(photoToUpdate, photoForMemberFromRepo);

            // From this moment on the entity already contains the updated fields!
            _photoLibraryRepository.UpdatePhoto(photoForMemberFromRepo);

            return NoContent();
        }
    }
}
