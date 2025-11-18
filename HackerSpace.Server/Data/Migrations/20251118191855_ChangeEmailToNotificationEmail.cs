using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HackerSpace.Migrations
{
    /// <inheritdoc />
    public partial class ChangeEmailToNotificationEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Evaluators",
                newName: "NotificationEmail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NotificationEmail",
                table: "Evaluators",
                newName: "Email");
        }
    }
}
