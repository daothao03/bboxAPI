using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautyBoxAPI.Migrations
{
    /// <inheritdoc />
    public partial class L16Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "hDNs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    NccId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hDNs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_hDNs_NhaCungCap_NccId",
                        column: x => x.NccId,
                        principalTable: "NhaCungCap",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_hDNs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chiTietHDN",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HdnId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    GiaNhap = table.Column<decimal>(type: "decimal(16,2)", precision: 16, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chiTietHDN", x => x.Id);
                    table.ForeignKey(
                        name: "FK_chiTietHDN_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_chiTietHDN_hDNs_HdnId",
                        column: x => x.HdnId,
                        principalTable: "hDNs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_chiTietHDN_HdnId",
                table: "chiTietHDN",
                column: "HdnId");

            migrationBuilder.CreateIndex(
                name: "IX_chiTietHDN_ProductId",
                table: "chiTietHDN",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_hDNs_NccId",
                table: "hDNs",
                column: "NccId");

            migrationBuilder.CreateIndex(
                name: "IX_hDNs_UserId",
                table: "hDNs",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chiTietHDN");

            migrationBuilder.DropTable(
                name: "hDNs");
        }
    }
}
