using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EuroCarsUSA.Migrations
{
    /// <inheritdoc />
    public partial class AddCarStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "Cars",
                newName: "TimeStamp");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "TimeStamp",
                table: "Cars",
                newName: "Timestamp");
        }
    }
}
