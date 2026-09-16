using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokenToUserToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeviceName",
                table: "UserTokens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireRefreshToken",
                table: "UserTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "UserTokens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceName",
                table: "UserTokens");

            migrationBuilder.DropColumn(
                name: "ExpireRefreshToken",
                table: "UserTokens");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "UserTokens");
        }
    }
}
