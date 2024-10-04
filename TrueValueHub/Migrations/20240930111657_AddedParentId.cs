using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrueValueHub.Migrations
{
    /// <inheritdoc />
    public partial class AddedParentId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Parts_ParentPartPartId",
                table: "Parts");

            migrationBuilder.AlterColumn<int>(
                name: "ParentId",
                table: "Parts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Parts_ParentPartPartId",
                table: "Parts",
                column: "ParentPartPartId",
                principalTable: "Parts",
                principalColumn: "PartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Parts_ParentPartPartId",
                table: "Parts");

            migrationBuilder.AlterColumn<int>(
                name: "ParentId",
                table: "Parts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Parts_ParentPartPartId",
                table: "Parts",
                column: "ParentPartPartId",
                principalTable: "Parts",
                principalColumn: "PartId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
