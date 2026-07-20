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
            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckDate",
                table: "HowToPays",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<bool>(
                name: "IsCheck",
                table: "DefinitiveAccounts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "SettledFromId",
                table: "DefinitiveAccounts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DefinitiveAccounts_SettledFromId",
                table: "DefinitiveAccounts",
                column: "SettledFromId");

            migrationBuilder.AddForeignKey(
                name: "FK_DefinitiveAccounts_DefinitiveAccounts_SettledFromId",
                table: "DefinitiveAccounts",
                column: "SettledFromId",
                principalTable: "DefinitiveAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DefinitiveAccounts_DefinitiveAccounts_SettledFromId",
                table: "DefinitiveAccounts");

            migrationBuilder.DropIndex(
                name: "IX_DefinitiveAccounts_SettledFromId",
                table: "DefinitiveAccounts");

            migrationBuilder.DropColumn(
                name: "IsCheck",
                table: "DefinitiveAccounts");

            migrationBuilder.DropColumn(
                name: "SettledFromId",
                table: "DefinitiveAccounts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckDate",
                table: "HowToPays",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
