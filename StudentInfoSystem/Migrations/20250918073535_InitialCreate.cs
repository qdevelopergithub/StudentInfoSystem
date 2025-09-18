using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentInfoSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Marks = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Grade = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Age", "Grade", "Marks", "Name" },
                values: new object[,]
                {
                    { 1, 20, "A", 85.5m, "Alice Johnson" },
                    { 2, 19, "B", 78.0m, "Bob Smith" },
                    { 3, 21, "A", 92.5m, "Carol Davis" },
                    { 4, 20, "C", 65.0m, "David Wilson" },
                    { 5, 22, "A", 88.5m, "Emma Brown" },
                    { 6, 19, "B", 72.0m, "Frank Miller" },
                    { 7, 20, "A", 95.0m, "Grace Taylor" },
                    { 8, 21, "D", 58.5m, "Henry Anderson" },
                    { 9, 19, "B", 82.0m, "Ivy Garcia" },
                    { 10, 20, "B", 76.5m, "Jack Martinez" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
