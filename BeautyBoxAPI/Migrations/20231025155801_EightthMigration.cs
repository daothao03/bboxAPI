using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautyBoxAPI.Migrations
{
    /// <inheritdoc />
    public partial class EightthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HoaDonNhap",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaNguoiDung = table.Column<int>(type: "int", nullable: false),
                    MaNCCID = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDonNhap", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HoaDonNhap_Suppliers_MaNCCID",
                        column: x => x.MaNCCID,
                        principalTable: "Suppliers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HoaDonNhap_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietHDN",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaSanPham = table.Column<int>(type: "int", nullable: false),
                    MaHdn = table.Column<int>(type: "int", nullable: false),
                    Soluong = table.Column<int>(type: "int", nullable: false),
                    GiaNhap = table.Column<decimal>(type: "decimal(16,2)", precision: 16, scale: 2, nullable: false),
                    sanphamId = table.Column<int>(type: "int", nullable: false),
                    hdnId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietHDN", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChiTietHDN_HoaDonNhap_hdnId",
                        column: x => x.hdnId,
                        principalTable: "HoaDonNhap",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietHDN_Products_sanphamId",
                        column: x => x.sanphamId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHDN_hdnId",
                table: "ChiTietHDN",
                column: "hdnId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHDN_sanphamId",
                table: "ChiTietHDN",
                column: "sanphamId");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDonNhap_MaNCCID",
                table: "HoaDonNhap",
                column: "MaNCCID");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDonNhap_UserId",
                table: "HoaDonNhap",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietHDN");

            migrationBuilder.DropTable(
                name: "HoaDonNhap");
        }
    }
}
