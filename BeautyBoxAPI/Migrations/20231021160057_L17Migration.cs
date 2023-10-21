using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautyBoxAPI.Migrations
{
    /// <inheritdoc />
    public partial class L17Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hDNs_NhaCungCap_NccId",
                table: "hDNs");

            migrationBuilder.RenameColumn(
                name: "NccId",
                table: "hDNs",
                newName: "NccIdID");

            migrationBuilder.RenameIndex(
                name: "IX_hDNs_NccId",
                table: "hDNs",
                newName: "IX_hDNs_NccIdID");

            migrationBuilder.AddForeignKey(
                name: "FK_hDNs_NhaCungCap_NccIdID",
                table: "hDNs",
                column: "NccIdID",
                principalTable: "NhaCungCap",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hDNs_NhaCungCap_NccIdID",
                table: "hDNs");

            migrationBuilder.RenameColumn(
                name: "NccIdID",
                table: "hDNs",
                newName: "NccId");

            migrationBuilder.RenameIndex(
                name: "IX_hDNs_NccIdID",
                table: "hDNs",
                newName: "IX_hDNs_NccId");

            migrationBuilder.AddForeignKey(
                name: "FK_hDNs_NhaCungCap_NccId",
                table: "hDNs",
                column: "NccId",
                principalTable: "NhaCungCap",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
