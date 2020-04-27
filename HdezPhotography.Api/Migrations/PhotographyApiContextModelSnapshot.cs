﻿// <auto-generated />
using System;
using HdezPhotography.Api.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HdezPhotography.Api.Migrations
{
    [DbContext(typeof(PhotographyApiContext))]
    partial class PhotographyApiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HdezPhotography.Api.Entities.Member", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("PhoneNum")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("ID");

                    b.ToTable("Members");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Email = "jonathan.hdez92@gmail.com",
                            Name = "Jonathan Hernandez",
                            PhoneNum = "(123) 456 7890"
                        },
                        new
                        {
                            ID = 2,
                            Email = "johndoe@gmail.com",
                            Name = "John Doe",
                            PhoneNum = "(123) 456 7891"
                        });
                });

            modelBuilder.Entity("HdezPhotography.Api.Entities.Photo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AlbumID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<int>("MemberID")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("View")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Photos");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            AlbumID = 1,
                            Description = "Cool Description",
                            ImagePath = "CLOUDINARY PATH",
                            MemberID = 1,
                            Title = "Sample",
                            UploadDate = new DateTime(2020, 4, 26, 0, 0, 0, 0, DateTimeKind.Local),
                            View = 0
                        },
                        new
                        {
                            ID = 2,
                            AlbumID = 1,
                            Description = "Cool Description2",
                            ImagePath = "CLOUDINARY PATH2",
                            MemberID = 1,
                            Title = "Sample2",
                            UploadDate = new DateTime(2020, 4, 26, 0, 0, 0, 0, DateTimeKind.Local),
                            View = 0
                        });
                });

            modelBuilder.Entity("HdezPhotography.Api.Entities.Tag", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("PhotoID")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("ID");

                    b.HasIndex("PhotoID");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("HdezPhotography.Api.Entities.Tag", b =>
                {
                    b.HasOne("HdezPhotography.Api.Entities.Photo", null)
                        .WithMany("Tags")
                        .HasForeignKey("PhotoID");
                });
#pragma warning restore 612, 618
        }
    }
}
