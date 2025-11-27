using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OPP_back.Migrations
{
    /// <inheritdoc />
    public partial class AddedTeam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Users_UserId",
                table: "Members");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Members",
                newName: "TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_Members_UserId",
                table: "Members",
                newName: "IX_Members_TeamId");

            migrationBuilder.AddColumn<Guid>(
                name: "TeamId",
                table: "Subjects",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Team_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_TeamId",
                table: "Subjects",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_UserId",
                table: "Team",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Team_TeamId",
                table: "Members",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Team_TeamId",
                table: "Subjects",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Team_TeamId",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Team_TeamId",
                table: "Subjects");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_TeamId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Subjects");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "Members",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Members_TeamId",
                table: "Members",
                newName: "IX_Members_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Users_UserId",
                table: "Members",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
