using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addnewentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Categoty_Id",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategotyId = table.Column<int>(name: "Categoty_Id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(name: "Category_Name", type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategotyId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_Categoty_Id",
                table: "Products",
                column: "Categoty_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_Categoty_Id",
                table: "Products",
                column: "Categoty_Id",
                principalTable: "Categories",
                principalColumn: "Categoty_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_Categoty_Id",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Products_Categoty_Id",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Categoty_Id",
                table: "Products");
        }
    }
}
