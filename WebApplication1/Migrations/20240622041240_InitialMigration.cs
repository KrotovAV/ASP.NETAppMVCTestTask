using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnDeleteAble = table.Column<bool>(type: "bit", nullable: false),
                    PriorityType = table.Column<int>(type: "int", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Category1" },
                    { 2, "Category2" },
                    { 3, "Category3" }
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "BirthDate", "CategoryId", "Email", "FirstName", "LastName", "Mobile", "PhotoPath", "PriorityType", "UnDeleteAble" },
                values: new object[,]
                {
                    { 1, new DateTime(2011, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "e1@e.e", "name1", "last1", "11", "/img/01.jpg", 0, false },
                    { 2, new DateTime(2010, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "e2@e.e", "name2", "last2", "22", "/img/02.jpg", 1, true },
                    { 3, new DateTime(2012, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "e3@e.e", "name3", "last3", "33", "/img/03.jpg", 2, false },
                    { 4, new DateTime(2013, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "e4@e.e", "name4", "last4", "4444", "/img/04.jpg", 3, false },
                    { 5, null, 3, "e5@e.e", "name5", "last5", "55", "/img/05.jpg", 3, false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CategoryId",
                table: "Contacts",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
