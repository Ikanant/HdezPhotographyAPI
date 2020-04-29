using HdezPhotography.Api.DbContexts;
using HdezPhotography.Api.Entities;
using HdezPhotography.Api.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HdezPhotography.Api.Services {
    public class PhotoLibraryRepository : IPhotoLibraryRepository, IDisposable {
        private readonly PhotographyApiContext _context;

        public PhotoLibraryRepository(PhotographyApiContext context) {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddPhoto(int memberID, Photo photo) {
            if (memberID == 0) {
                throw new ArgumentNullException(nameof(memberID));
            }

            if (photo == null) {
                throw new ArgumentNullException(nameof(photo));
            }
            // always set the MemberID to the passed-in memberID
            photo.MemberID = memberID;
            photo.UploadDate = DateTime.Now;
            _context.Photos.Add(photo);
        }

        public void DeletePhoto(Photo photo) {
            _context.Photos.Remove(photo);
        }

        public Photo GetPhoto(int memberID, int photoID) {
            if (memberID == 0) {
                throw new ArgumentNullException(nameof(memberID));
            }

            if (photoID == 0) {
                throw new ArgumentNullException(nameof(photoID));
            }

            return _context.Photos
              .Where(c => c.MemberID == memberID && c.ID == photoID).FirstOrDefault();
        }

        public IEnumerable<Photo> GetPhotos(int memberID) {
            if (memberID == 0) {
                throw new ArgumentNullException(nameof(memberID));
            }

            return _context.Photos
                        .Where(c => c.MemberID == memberID)
                        .OrderBy(c => c.Title).ToList();
        }

        public IEnumerable<Photo> GetPhotos(int memberID, PhotosResourceParameters photosResourceParameters) {
            if (photosResourceParameters == null) {
                throw new ArgumentNullException(nameof(photosResourceParameters));
            }
            // Dealing with IQueryable allows us to pile where clause sections without loading the data.
            // Pulling the data and filtering it is insanely nonperformant
            var collection = _context.Photos as IQueryable<Photo>;
             
            if (photosResourceParameters.ViewCount > 0) {
                collection = collection.Where(c => c.MemberID == memberID && c.Views >= photosResourceParameters.ViewCount);
            }

            if (!string.IsNullOrWhiteSpace(photosResourceParameters.SearchQuery)) {
                var searchQuery = photosResourceParameters.SearchQuery.Trim();

                collection = collection.Where(w => w.Description.Contains(searchQuery));
            }

            return collection.ToList();
        }

        public void UpdatePhoto(Photo photo) {
            // no code in this implementation
        }

        public void AddMember(Member member) {
            if (member == null) {
                throw new ArgumentNullException(nameof(member));
            }

            // the repository fills the id (instead of using identity columns)
            // member.ID = int.Newint();

            _context.Members.Add(member);
        }

        public bool MemberExists(int memberID) {
            if (memberID == 0) {
                throw new ArgumentNullException(nameof(memberID));
            }

            return _context.Members.Any(a => a.ID == memberID);
        }

        public void DeleteMember(Member member) {
            if (member == null) {
                throw new ArgumentNullException(nameof(member));
            }

            _context.Members.Remove(member);
        }

        public Member GetMember(int memberID) {
            if (memberID == 0) {
                throw new ArgumentNullException(nameof(memberID));
            }

            return _context.Members.FirstOrDefault(a => a.ID == memberID);
        }

        public IEnumerable<Member> GetMembers() {
            return _context.Members.ToList<Member>();
        }

        public IEnumerable<Member> GetMembers(IEnumerable<int> memberIDs) {
            if (memberIDs == null) {
                throw new ArgumentNullException(nameof(memberIDs));
            }

            return _context.Members.Where(a => memberIDs.Contains(a.ID))
                .OrderBy(a => a.Name)
                .ToList();
        }

        public void UpdateMember(Member member) {
            // no code in this implementation
        }

        public bool Save() {
            return (_context.SaveChanges() >= 0);
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                // dispose resources when needed
            }
        }
    }
}
