using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AircraftRepair.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedTaskStates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TaskStates",
                columns: new[] { "IdState", "Code", "Value" },
                values: new object[,]
                {
                    { 1, 1, "pending" },
                    { 2, 2, "inProgress" },
                    { 3, 3, "done" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TaskStates",
                keyColumn: "IdState",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TaskStates",
                keyColumn: "IdState",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TaskStates",
                keyColumn: "IdState",
                keyValue: 3);
        }
    }
}
