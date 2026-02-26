using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AircraftRepair.Migrations
{
    /// <inheritdoc />
    public partial class FixPermissionForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_Permissions_PermissionIdPermission",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_PermissionIdPermission",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "PermissionIdPermission",
                table: "AppUsers");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_IdPermission",
                table: "AppUsers",
                column: "IdPermission");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_Permissions_IdPermission",
                table: "AppUsers",
                column: "IdPermission",
                principalTable: "Permissions",
                principalColumn: "IdPermission",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_Permissions_IdPermission",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_IdPermission",
                table: "AppUsers");

            migrationBuilder.AddColumn<int>(
                name: "PermissionIdPermission",
                table: "AppUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_PermissionIdPermission",
                table: "AppUsers",
                column: "PermissionIdPermission");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_Permissions_PermissionIdPermission",
                table: "AppUsers",
                column: "PermissionIdPermission",
                principalTable: "Permissions",
                principalColumn: "IdPermission",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
