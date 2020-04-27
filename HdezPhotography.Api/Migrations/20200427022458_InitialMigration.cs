using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HdezPhotography.Api.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    PhoneNum = table.Column<string>(maxLength: 200, nullable: false),
                    Email = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlbumID = table.Column<int>(nullable: false),
                    MemberID = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    UploadDate = table.Column<DateTime>(nullable: false),
                    View = table.Column<int>(nullable: false),
                    ImagePath = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 200, nullable: false),
                    PhotoID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tag_Photos_PhotoID",
                        column: x => x.PhotoID,
                        principalTable: "Photos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "ID", "Email", "Name", "PhoneNum" },
                values: new object[,]
                {
                    { 1, "jonathan.hdez92@gmail.com", "Jonathan Hernandez", "(123) 456 7890" },
                    { 2, "johndoe@gmail.com", "John Doe", "(123) 456 7891" }
                });

            migrationBuilder.InsertData(
                table: "Photos",
                columns: new[] { "ID", "AlbumID", "Description", "ImagePath", "MemberID", "Title", "UploadDate", "View" },
                values: new object[,]
                {
                    { 1, 1, "Cool Description", "CLOUDINARY PATH", 1, "Sample", new DateTime(2020, 4, 26, 0, 0, 0, 0, DateTimeKind.Local), 0 },
                    { 2, 1, "Cool Description2", "CLOUDINARY PATH2", 1, "Sample2", new DateTime(2020, 4, 26, 0, 0, 0, 0, DateTimeKind.Local), 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tag_PhotoID",
                table: "Tag",
                column: "PhotoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Photos");
        }
    }
}
