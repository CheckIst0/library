using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseWork.Migrations
{
    /// <inheritdoc />
    public partial class ChangerEntryHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "EntryHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EntryHistories_UserId",
                table: "EntryHistories",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EntryHistories_Users_UserId",
                table: "EntryHistories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntryHistories_Users_UserId",
                table: "EntryHistories");

            migrationBuilder.DropIndex(
                name: "IX_EntryHistories_UserId",
                table: "EntryHistories");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "EntryHistories");
        }
    }
}
