using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrueValueHub.Migrations
{
    /// <inheritdoc />
    public partial class partComplexityadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Parts_ParentPartPartId",
                table: "Parts");

            migrationBuilder.AddColumn<int>(
                name: "PartComplexity",
                table: "Parts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Parts_ParentPartPartId",
                table: "Parts",
                column: "ParentPartPartId",
                principalTable: "Parts",
                principalColumn: "PartId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Parts_ParentPartPartId",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "PartComplexity",
                table: "Parts");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Parts_ParentPartPartId",
                table: "Parts",
                column: "ParentPartPartId",
                principalTable: "Parts",
                principalColumn: "PartId");
        }
    }
}
