using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HackerSpace.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEvaluatorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "First",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Last",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Middle",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Evaluators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    NotificationEmail = table.Column<string>(type: "TEXT", nullable: true)
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
