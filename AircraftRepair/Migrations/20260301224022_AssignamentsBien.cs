using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AircraftRepair.Migrations
{
    /// <inheritdoc />
    public partial class AssignamentsBien : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_AppUsers_AppUserIdUser",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Tasks_TaskItemIdTask",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "TaskItemIdTask",
                table: "Assignments",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Assignments_TaskItemIdTask",
                table: "Assignments",
                newName: "IX_Assignments_AppUserId");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserIdUser",
                table: "Assignments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_IdTask",
                table: "Assignments",
                column: "IdTask");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_AppUsers_AppUserIdUser",
                table: "Assignments",
                column: "AppUserIdUser",
                principalTable: "AppUsers",
                principalColumn: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_AppUsers_IdUser",
                table: "Assignments",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Tasks_IdTask",
                table: "Assignments",
                column: "IdTask",
                principalTable: "Tasks",
                principalColumn: "IdTask",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_AppUsers_AppUserIdUser",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_AppUsers_IdUser",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Tasks_IdTask",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_IdTask",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Assignments",
                newName: "TaskItemIdTask");

            migrationBuilder.RenameIndex(
                name: "IX_Assignments_AppUserId",
                table: "Assignments",
                newName: "IX_Assignments_TaskItemIdTask");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserIdUser",
                table: "Assignments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_AppUsers_AppUserIdUser",
                table: "Assignments",
                column: "AppUserIdUser",
                principalTable: "AppUsers",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Tasks_TaskItemIdTask",
                table: "Assignments",
                column: "TaskItemIdTask",
                principalTable: "Tasks",
                principalColumn: "IdTask",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
