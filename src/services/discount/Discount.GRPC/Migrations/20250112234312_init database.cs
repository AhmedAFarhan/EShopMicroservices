using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Discount.GRPC.Migrations
{
    /// <inheritdoc />
    public partial class initdatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cuopons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductName = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Amount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuopons", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Cuopons",
                columns: new[] { "Id", "Amount", "Description", "ProductName" },
                values: new object[,]
                {
                    { 1, 150, "IPhone discount", "IPhone X" },
                    { 2, 300, "Samsung discount", "Samsung 10" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cuopons");
        }
    }
}
