using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chatbotplan.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailChangeCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailChangeCodeExpiry",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailChangeCode",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EmailChangeCodeExpiry",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);
        }
    }
}
