using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EuroCarsUSA.Migrations
{
    /// <inheritdoc />
    public partial class AddMultipleFormsToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormCarMakes_Forms_FormId",
                table: "FormCarMakes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FormCarMakes",
                table: "FormCarMakes");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Forms");

            migrationBuilder.RenameTable(
                name: "FormCarMakes",
                newName: "FormCarMake");

            migrationBuilder.RenameIndex(
                name: "IX_FormCarMakes_FormId",
                table: "FormCarMake",
                newName: "IX_FormCarMake_FormId");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomOrderId",
                table: "Forms",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormCarMake",
                table: "FormCarMake",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CustomOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomOrders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Forms_CustomOrderId",
                table: "Forms",
                column: "CustomOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_FormCarMake_Forms_FormId",
                table: "FormCarMake",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Forms_CustomOrders_CustomOrderId",
                table: "Forms",
                column: "CustomOrderId",
                principalTable: "CustomOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormCarMake_Forms_FormId",
                table: "FormCarMake");

            migrationBuilder.DropForeignKey(
                name: "FK_Forms_CustomOrders_CustomOrderId",
                table: "Forms");

            migrationBuilder.DropTable(
                name: "CustomOrders");

            migrationBuilder.DropIndex(
                name: "IX_Forms_CustomOrderId",
                table: "Forms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FormCarMake",
                table: "FormCarMake");

            migrationBuilder.DropColumn(
                name: "CustomOrderId",
                table: "Forms");

            migrationBuilder.RenameTable(
                name: "FormCarMake",
                newName: "FormCarMakes");

            migrationBuilder.RenameIndex(
                name: "IX_FormCarMake_FormId",
                table: "FormCarMakes",
                newName: "IX_FormCarMakes_FormId");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Forms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Forms",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Forms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Forms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormCarMakes",
                table: "FormCarMakes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormCarMakes_Forms_FormId",
                table: "FormCarMakes",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
