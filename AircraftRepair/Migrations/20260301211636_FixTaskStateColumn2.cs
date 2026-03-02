using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AircraftRepair.Migrations
{
    /// <inheritdoc />
    public partial class FixTaskStateColumn2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_TaskStates_TaskStateIdState",
                table: "Tasks");

            migrationBuilder.AlterColumn<int>(
                name: "TaskStateIdState",
                table: "Tasks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_IdState",
                table: "Tasks",
                column: "IdState");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_TaskStates_IdState",
                table: "Tasks",
                column: "IdState",
                principalTable: "TaskStates",
                principalColumn: "IdState",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_TaskStates_TaskStateIdState",
                table: "Tasks",
                column: "TaskStateIdState",
                principalTable: "TaskStates",
                principalColumn: "IdState");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_TaskStates_IdState",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_TaskStates_TaskStateIdState",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_IdState",
                table: "Tasks");

            migrationBuilder.AlterColumn<int>(
                name: "TaskStateIdState",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_TaskStates_TaskStateIdState",
                table: "Tasks",
                column: "TaskStateIdState",
                principalTable: "TaskStates",
                principalColumn: "IdState",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
