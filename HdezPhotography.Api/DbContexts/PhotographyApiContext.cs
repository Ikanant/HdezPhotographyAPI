using HdezPhotography.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace HdezPhotography.Api.DbContexts {
    public class PhotographyApiContext : DbContext {
        public PhotographyApiContext(DbContextOptions<PhotographyApiContext> options)
           : base(options) {
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Photo> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            // seed the database with dummy data
            modelBuilder.Entity<Member>().HasData(
                new Member() {
                    ID = 1,
                    Name = "Jonathan Hernandez",
                    PhoneNum = "(123) 456 7890",
                    Email = "jonathan.hdez92@gmail.com"
                },
                new Member() {
                    ID = 2,
                    Name = "John Doe",
                    PhoneNum = "(123) 456 7891",
                    Email = "johndoe@gmail.com"
                }
            );

            modelBuilder.Entity<Photo>().HasData(
               new Photo {
                    ID = 1,
                    AlbumID = 1,
                    MemberID = 1,
                    Title = "Sample",
                    Description = "Cool Description",
                    UploadDate = DateTime.Today,
                    Views = 123,
                    ImagePath = "CLOUDINARY PATH"
                },
               new Photo {
                   ID = 2,
                   AlbumID = 1,
                   MemberID = 1,
                   Title = "Sample2",
                   Description = "Cool Description2",
                   UploadDate = DateTime.Today,
                   Views = 12412,
                   ImagePath = "CLOUDINARY PATH2"
               }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
