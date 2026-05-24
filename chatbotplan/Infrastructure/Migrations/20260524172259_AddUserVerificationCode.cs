using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chatbotplan.Migrations
{
    /// <inheritdoc />
    public partial class AddUserVerificationCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "UserVerificationCodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    Expiration = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsUsed = table.Column<bool>(type: "boolean", nullable: false),
                    PendingValue = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVerificationCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserVerificationCodes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserVerificationCodes_UserId",
                table: "UserVerificationCodes",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserVerificationCodes");

            migrationBuilder.DropColumn(
                name: "EmailChangeCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailChangeCodeExpiry",
                table: "Users");
        }
    }
}
