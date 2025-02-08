using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballApp.Migrations
{
    /// <inheritdoc />
    public partial class CaptionRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayerIds",
                table: "Teams");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "Teams",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "TeamBScore",
                table: "Matches",
                newName: "TeamScore");

            migrationBuilder.RenameColumn(
                name: "TeamBId",
                table: "Matches",
                newName: "TeamId");

            migrationBuilder.RenameColumn(
                name: "TeamAScore",
                table: "Matches",
                newName: "EnemyScore");

            migrationBuilder.RenameColumn(
                name: "TeamAId",
                table: "Matches",
                newName: "EnemyId");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Matches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "TeamMembers",
                columns: table => new
                {
                    MembersId = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMembers", x => new { x.MembersId, x.TeamId });
                    table.ForeignKey(
                        name: "FK_TeamMembers_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamMembers_Users_MembersId",
                        column: x => x.MembersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CaptainId",
                table: "Teams",
                column: "CaptainId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMembers_TeamId",
                table: "TeamMembers",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Users_CaptainId",
                table: "Teams",
                column: "CaptainId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Users_CaptainId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "TeamMembers");

            migrationBuilder.DropIndex(
                name: "IX_Teams_CaptainId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Matches");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Teams",
                newName: "DateCreated");

            migrationBuilder.RenameColumn(
                name: "TeamScore",
                table: "Matches",
                newName: "TeamBScore");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "Matches",
                newName: "TeamBId");

            migrationBuilder.RenameColumn(
                name: "EnemyScore",
                table: "Matches",
                newName: "TeamAScore");

            migrationBuilder.RenameColumn(
                name: "EnemyId",
                table: "Matches",
                newName: "TeamAId");

            migrationBuilder.AddColumn<string>(
                name: "PlayerIds",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
