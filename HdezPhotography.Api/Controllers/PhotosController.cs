using AutoMapper;
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

        [HttpGet]
        public ActionResult<IEnumerable<PhotoDto>> GetPhotosForMember(int memberID, [FromQuery] PhotosResourceParameters photosResourceParameters) {
            if (!_photoLibraryRepository.MemberExists(memberID)) {
                return NotFound();
            }

            var photosFromRepo = _photoLibraryRepository.GetPhotos(memberID, photosResourceParameters);
            return Ok(_mapper.Map<IEnumerable<PhotoDto>>(photosFromRepo));
        }

        [HttpGet("{photoID:int}")]
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
    }
}
