using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QueueManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTokenTransfer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TokenTransfers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TokenId = table.Column<int>(type: "int", nullable: false),
                    FromCategoryId = table.Column<int>(type: "int", nullable: false),
                    FromSubCategoryId = table.Column<int>(type: "int", nullable: true),
                    ToCategoryId = table.Column<int>(type: "int", nullable: false),
                    ToSubCategoryId = table.Column<int>(type: "int", nullable: true),
                    TransferReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransferedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransferedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokenTransfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TokenTransfers_Categories_FromCategoryId",
                        column: x => x.FromCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TokenTransfers_Categories_ToCategoryId",
                        column: x => x.ToCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TokenTransfers_SubCategories_FromSubCategoryId",
                        column: x => x.FromSubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TokenTransfers_SubCategories_ToSubCategoryId",
                        column: x => x.ToSubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TokenTransfers_Tokens_TokenId",
                        column: x => x.TokenId,
                        principalTable: "Tokens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TokenTransfers_FromCategoryId",
                table: "TokenTransfers",
                column: "FromCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TokenTransfers_FromSubCategoryId",
                table: "TokenTransfers",
                column: "FromSubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TokenTransfers_ToCategoryId",
                table: "TokenTransfers",
                column: "ToCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TokenTransfers_TokenId",
                table: "TokenTransfers",
                column: "TokenId");

            migrationBuilder.CreateIndex(
                name: "IX_TokenTransfers_ToSubCategoryId",
                table: "TokenTransfers",
                column: "ToSubCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TokenTransfers");
        }
    }
}
