using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QueueManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTokenEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedAt",
                table: "Tokens",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSkipped",
                table: "Tokens",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "QueueNumber",
                table: "Tokens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ServingStartedAt",
                table: "Tokens",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedAt",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "IsSkipped",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "QueueNumber",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "ServingStartedAt",
                table: "Tokens");
        }
    }
}
