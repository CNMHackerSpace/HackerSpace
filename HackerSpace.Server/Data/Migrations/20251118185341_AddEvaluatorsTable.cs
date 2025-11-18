using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HackerSpace.Migrations
{
    /// <inheritdoc />
    public partial class AddEvaluatorsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "First",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Last",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Middle",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Evaluators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evaluators_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evaluators_UserId",
                table: "Evaluators",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Evaluators");

            migrationBuilder.DropColumn(
                name: "First",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Last",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Middle",
                table: "AspNetUsers");
        }
    }
}
