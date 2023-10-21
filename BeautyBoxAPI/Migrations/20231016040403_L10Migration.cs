using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautyBoxAPI.Migrations
{
    /// <inheritdoc />
    public partial class L10Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Users_UserID",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_UserID",
                table: "Blogs");

            migrationBuilder.CreateTable(
                name: "Promotions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Percent = table.Column<decimal>(type: "decimal(16,2)", precision: 16, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Promotions");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_UserID",
                table: "Blogs",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Users_UserID",
                table: "Blogs",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
