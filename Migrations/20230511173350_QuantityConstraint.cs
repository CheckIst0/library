using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseWork.Migrations
{
    /// <inheritdoc />
    public partial class QuantityConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_Book_Quantity",
                table: "Books",
                sql: "Quantity > 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Book_Quantity",
                table: "Books");
        }
    }
}
