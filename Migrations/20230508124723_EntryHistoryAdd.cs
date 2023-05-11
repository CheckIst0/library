using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseWork.Migrations
{
    /// <inheritdoc />
    public partial class EntryHistoryAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Review_Rating",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "ReturnDate",
                table: "IssueHistories",
                newName: "EstimatedReturnDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "FactReturnDate",
                table: "IssueHistories",
                type: "Date",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EntryHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryHistories", x => x.Id);
                });

            migrationBuilder.AddCheckConstraint(
                name: "CK_Review_Rating",
                table: "Reviews",
                sql: "Rating > 0 AND Rating < 6");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntryHistories");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Review_Rating",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "FactReturnDate",
                table: "IssueHistories");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "EstimatedReturnDate",
                table: "IssueHistories",
                newName: "ReturnDate");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Review_Rating",
                table: "Reviews",
                sql: "Rating > 0 AND Rating < 5");
        }
    }
}
