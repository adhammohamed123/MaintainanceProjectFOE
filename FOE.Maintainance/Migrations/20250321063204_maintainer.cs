using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FOE.Maintainance.Migrations
{
    /// <inheritdoc />
    public partial class maintainer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maintainances_Stuffs_MaintainerId",
                table: "Maintainances");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Maintainances",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "MaintainerId",
                table: "Maintainances",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Maintainances_Stuffs_MaintainerId",
                table: "Maintainances",
                column: "MaintainerId",
                principalTable: "Stuffs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maintainances_Stuffs_MaintainerId",
                table: "Maintainances");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Maintainances",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MaintainerId",
                table: "Maintainances",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Maintainances_Stuffs_MaintainerId",
                table: "Maintainances",
                column: "MaintainerId",
                principalTable: "Stuffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
