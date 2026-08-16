using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kavosh.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MarketerId",
                table: "FactorHeaders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Marketers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marketers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FactorHeaders_MarketerId",
                table: "FactorHeaders",
                column: "MarketerId");

            migrationBuilder.AddForeignKey(
                name: "FK_FactorHeaders_Marketers_MarketerId",
                table: "FactorHeaders",
                column: "MarketerId",
                principalTable: "Marketers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FactorHeaders_Marketers_MarketerId",
                table: "FactorHeaders");

            migrationBuilder.DropTable(
                name: "Marketers");

            migrationBuilder.DropIndex(
                name: "IX_FactorHeaders_MarketerId",
                table: "FactorHeaders");

            migrationBuilder.DropColumn(
                name: "MarketerId",
                table: "FactorHeaders");
        }
    }
}
