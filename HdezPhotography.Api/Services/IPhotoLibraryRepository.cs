using HdezPhotography.Api.Entities;
using System;
using System.Collections.Generic;

namespace HdezPhotography.Api.Services {
    public interface IPhotoLibraryRepository {
        IEnumerable<Photo> GetPhotos(int memberId);
        Photo GetPhoto(int memberId, int photoId);
        void AddPhoto(int memberId, Photo photo);
        void UpdatePhoto(Photo photo);
        void DeletePhoto(Photo photo);
        IEnumerable<Member> GetMembers();
        Member GetMember(int memberId);
        IEnumerable<Member> GetMembers(IEnumerable<int> memberIds);
        void AddMember(Member member);
        void DeleteMember(Member member);
        void UpdateMember(Member member);
        bool MemberExists(int memberId);
        bool Save();
    }
}
