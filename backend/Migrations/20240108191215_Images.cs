using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VGL_Project.Migrations
{
    /// <inheritdoc />
    public partial class Images : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventParticipants_Events_EventId",
                table: "EventParticipants");

            migrationBuilder.RenameColumn(
                name: "SteamId",
                table: "Users",
                newName: "steam_id");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "EventParticipants",
                newName: "SventId");

            migrationBuilder.RenameIndex(
                name: "IX_EventParticipants_EventId",
                table: "EventParticipants",
                newName: "IX_EventParticipants_SventId");

            migrationBuilder.AddColumn<byte[]>(
                name: "EventImage",
                table: "Events",
                type: "longblob",
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_EventParticipants_Events_SventId",
                table: "EventParticipants",
                column: "SventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventParticipants_Events_SventId",
                table: "EventParticipants");

            migrationBuilder.DropColumn(
                name: "EventImage",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "steam_id",
                table: "Users",
                newName: "SteamId");

            migrationBuilder.RenameColumn(
                name: "SventId",
                table: "EventParticipants",
                newName: "EventId");

            migrationBuilder.RenameIndex(
                name: "IX_EventParticipants_SventId",
                table: "EventParticipants",
                newName: "IX_EventParticipants_EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventParticipants_Events_EventId",
                table: "EventParticipants",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
