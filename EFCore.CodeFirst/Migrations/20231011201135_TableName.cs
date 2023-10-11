using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.CodeFirst.Migrations
{
    /// <inheritdoc />
    public partial class TableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductTBB",
                schema: "ProductBbb",
                table: "ProductTBB");

            migrationBuilder.EnsureSchema(
                name: "products");

            migrationBuilder.RenameTable(
                name: "ProductTBB",
                schema: "ProductBbb",
                newName: "ProductTb",
                newSchema: "products");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTb",
                schema: "products",
                table: "ProductTb",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductTb",
                schema: "products",
                table: "ProductTb");

            migrationBuilder.EnsureSchema(
                name: "ProductBbb");

            migrationBuilder.RenameTable(
                name: "ProductTb",
                schema: "products",
                newName: "ProductTBB",
                newSchema: "ProductBbb");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTBB",
                schema: "ProductBbb",
                table: "ProductTBB",
                column: "Id");
        }
    }
}
