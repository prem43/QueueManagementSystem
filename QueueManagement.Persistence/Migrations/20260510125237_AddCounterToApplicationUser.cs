using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QueueManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCounterToApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CounterId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CounterId",
                table: "AspNetUsers",
                column: "CounterId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Counters_CounterId",
                table: "AspNetUsers",
                column: "CounterId",
                principalTable: "Counters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Counters_CounterId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CounterId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CounterId",
                table: "AspNetUsers");
        }
    }
}
