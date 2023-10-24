using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautyBoxAPI.Migrations
{
    /// <inheritdoc />
    public partial class SixthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "hoaDonNhaps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaNguoiDung = table.Column<int>(type: "int", nullable: false),
                    MaNCC = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    nccID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hoaDonNhaps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_hoaDonNhaps_Suppliers_nccID",
                        column: x => x.nccID,
                        principalTable: "Suppliers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_hoaDonNhaps_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chiTietHoaDonNhaps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaSanPham = table.Column<int>(type: "int", nullable: false),
                    MaHoaDonNhap = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    GiaNhap = table.Column<decimal>(type: "decimal(16,2)", precision: 16, scale: 2, nullable: false),
                    sanphamId = table.Column<int>(type: "int", nullable: false),
                    hdnId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chiTietHoaDonNhaps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_chiTietHoaDonNhaps_Products_sanphamId",
                        column: x => x.sanphamId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_chiTietHoaDonNhaps_hoaDonNhaps_hdnId",
                        column: x => x.hdnId,
                        principalTable: "hoaDonNhaps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_chiTietHoaDonNhaps_hdnId",
                table: "chiTietHoaDonNhaps",
                column: "hdnId");

            migrationBuilder.CreateIndex(
                name: "IX_chiTietHoaDonNhaps_sanphamId",
                table: "chiTietHoaDonNhaps",
                column: "sanphamId");

            migrationBuilder.CreateIndex(
                name: "IX_hoaDonNhaps_nccID",
                table: "hoaDonNhaps",
                column: "nccID");

            migrationBuilder.CreateIndex(
                name: "IX_hoaDonNhaps_UserId",
                table: "hoaDonNhaps",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chiTietHoaDonNhaps");

            migrationBuilder.DropTable(
                name: "hoaDonNhaps");
        }
    }
}
