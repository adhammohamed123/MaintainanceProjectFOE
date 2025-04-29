using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FOE.Maintainance.Migrations
{
    /// <inheritdoc />
    public partial class updateDeviceNullableDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Devices_MAC",
                table: "Devices");

            migrationBuilder.AlterColumn<string>(
                name: "RAMTotal",
                table: "Devices",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(8)",
                oldMaxLength: 8);

            migrationBuilder.AlterColumn<string>(
                name: "MAC",
                table: "Devices",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "GPU",
                table: "Devices",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "CPU",
                table: "Devices",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "41DE9DCE-5A19-4C25-B336-8BA113BC9886",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2ae96f96-32c8-47b7-9279-7c4ce9b43cd3", "AQAAAAIAAYagAAAAEJA6POA/vkGIqPwzntxQ8UgZoLVakfBj84LLb8iIwU3VxTZKLy50h3aJr3f8D0t9eA==", "eb4ca4a1-d29e-4d28-9389-0add70fc7f3b" });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_MAC",
                table: "Devices",
                column: "MAC",
                unique: true,
                filter: "[MAC] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Devices_MAC",
                table: "Devices");

            migrationBuilder.AlterColumn<string>(
                name: "RAMTotal",
                table: "Devices",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(8)",
                oldMaxLength: 8,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MAC",
                table: "Devices",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GPU",
                table: "Devices",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CPU",
                table: "Devices",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "41DE9DCE-5A19-4C25-B336-8BA113BC9886",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "45230878-ec1b-4451-8ff2-6d478dc38087", "AQAAAAIAAYagAAAAEGGU+Xai0VKpA58pH1xJTm3nY9q8mJtS4cykfdAHJJbdobLowb7m/fzDoKoYIq8hag==", "48716c96-8af0-4dbf-93fd-0d4eb3f4a855" });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_MAC",
                table: "Devices",
                column: "MAC",
                unique: true);
        }
    }
}
