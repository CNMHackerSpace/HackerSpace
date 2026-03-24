// Copyright (c) 2025. All rights reserved.

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBadgeSuggestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BadgeSuggestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 5000, nullable: false),
                    TurnInInstructions = table.Column<string>(type: "TEXT", maxLength: 5000, nullable: false),
                    SuggestedById = table.Column<string>(type: "TEXT", nullable: false),
                    SuggestedByName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ReviewedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ReviewedById = table.Column<string>(type: "TEXT", nullable: true),
                    AdminNotes = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BadgeSuggestions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BadgeSuggestions");
        }
    }
}
