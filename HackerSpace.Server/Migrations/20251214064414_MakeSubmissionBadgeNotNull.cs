using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HackerSpace.Server.Migrations
{
    /// <inheritdoc />
    public partial class MakeSubmissionBadgeNotNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Badges_BadgeId",
                table: "Submissions");

            migrationBuilder.AlterColumn<Guid>(
                name: "BadgeId",
                table: "Submissions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Badges_BadgeId",
                table: "Submissions",
                column: "BadgeId",
                principalTable: "Badges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Badges_BadgeId",
                table: "Submissions");

            migrationBuilder.AlterColumn<Guid>(
                name: "BadgeId",
                table: "Submissions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Badges_BadgeId",
                table: "Submissions",
                column: "BadgeId",
                principalTable: "Badges",
                principalColumn: "Id");
        }
    }
}
