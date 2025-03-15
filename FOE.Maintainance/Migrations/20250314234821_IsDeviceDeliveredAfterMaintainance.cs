using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FOE.Maintainance.Migrations
{
    /// <inheritdoc />
    public partial class IsDeviceDeliveredAfterMaintainance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeliverd",
                table: "Maintainances",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeliverd",
                table: "Maintainances");
        }
    }
}
