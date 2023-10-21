using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautyBoxAPI.Migrations
{
    /// <inheritdoc />
    public partial class L18Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hDNs_Users_UserId",
                table: "hDNs");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "hDNs",
                newName: "UserIdId");

            migrationBuilder.RenameIndex(
                name: "IX_hDNs_UserId",
                table: "hDNs",
                newName: "IX_hDNs_UserIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_hDNs_Users_UserIdId",
                table: "hDNs",
                column: "UserIdId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hDNs_Users_UserIdId",
                table: "hDNs");

            migrationBuilder.RenameColumn(
                name: "UserIdId",
                table: "hDNs",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_hDNs_UserIdId",
                table: "hDNs",
                newName: "IX_hDNs_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_hDNs_Users_UserId",
                table: "hDNs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
