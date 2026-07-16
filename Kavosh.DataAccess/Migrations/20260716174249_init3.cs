using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kavosh.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FactorHeaderId",
                table: "HowToPays",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "HowToPayId",
                table: "DefinitiveAccounts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HowToPays_FactorHeaderId",
                table: "HowToPays",
                column: "FactorHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_HowToPays_PaymentTypeId",
                table: "HowToPays",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DefinitiveAccounts_HowToPayId",
                table: "DefinitiveAccounts",
                column: "HowToPayId");

            migrationBuilder.AddForeignKey(
                name: "FK_DefinitiveAccounts_HowToPays_HowToPayId",
                table: "DefinitiveAccounts",
                column: "HowToPayId",
                principalTable: "HowToPays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HowToPays_FactorHeaders_FactorHeaderId",
                table: "HowToPays",
                column: "FactorHeaderId",
                principalTable: "FactorHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HowToPays_PaymentTypes_PaymentTypeId",
                table: "HowToPays",
                column: "PaymentTypeId",
                principalTable: "PaymentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DefinitiveAccounts_HowToPays_HowToPayId",
                table: "DefinitiveAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_HowToPays_FactorHeaders_FactorHeaderId",
                table: "HowToPays");

            migrationBuilder.DropForeignKey(
                name: "FK_HowToPays_PaymentTypes_PaymentTypeId",
                table: "HowToPays");

            migrationBuilder.DropTable(
                name: "PaymentTypes");

            migrationBuilder.DropIndex(
                name: "IX_HowToPays_FactorHeaderId",
                table: "HowToPays");

            migrationBuilder.DropIndex(
                name: "IX_HowToPays_PaymentTypeId",
                table: "HowToPays");

            migrationBuilder.DropIndex(
                name: "IX_DefinitiveAccounts_HowToPayId",
                table: "DefinitiveAccounts");

            migrationBuilder.DropColumn(
                name: "FactorHeaderId",
                table: "HowToPays");

            migrationBuilder.DropColumn(
                name: "HowToPayId",
                table: "DefinitiveAccounts");
        }
    }
}
