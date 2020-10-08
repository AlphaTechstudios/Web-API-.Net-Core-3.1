using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RefreshToken.Context.Migrations
{
    public partial class AddToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenExpires",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TokenExpires",
                table: "Users");
        }
    }
}
