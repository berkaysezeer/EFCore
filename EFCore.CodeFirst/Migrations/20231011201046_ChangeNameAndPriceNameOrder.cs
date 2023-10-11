using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.CodeFirst.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNameAndPriceNameOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "Price",
                schema: "ProductBbb",
                table: "ProductTBB",
                newName: "PriceValue");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "ProductBbb",
                table: "ProductTBB",
                newName: "Name2");

            migrationBuilder.AlterColumn<decimal>(
                name: "PriceValue",
                schema: "ProductBbb",
                table: "ProductTBB",
                type: "decimal(15,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)")
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<string>(
                name: "Name2",
                schema: "ProductBbb",
                table: "ProductTBB",
                type: "nvarchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)")
                .Annotation("Relational:ColumnOrder", 3);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTBB",
                schema: "ProductBbb",
                table: "ProductTBB",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "PriceValue",
                schema: "products",
                table: "ProductTb",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "Name2",
                schema: "products",
                table: "ProductTb",
                newName: "Name");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                schema: "products",
                table: "ProductTb",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,2)")
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "products",
                table: "ProductTb",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)")
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTb",
                schema: "products",
                table: "ProductTb",
                column: "Id");
        }
    }
}
