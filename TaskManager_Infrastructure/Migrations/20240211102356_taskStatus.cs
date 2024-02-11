using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskManager_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class taskStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskStatus",
                columns: table => new
                {
                    TaskStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskStatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskStatus", x => x.TaskStatusId);
                });

            migrationBuilder.InsertData(
                table: "TaskStatus",
                columns: new[] { "TaskStatusId", "TaskStatusName" },
                values: new object[,]
                {
                    { 1, "Holding" },
                    { 2, "Prioritized" },
                    { 3, "Started" },
                    { 4, "Finished" },
                    { 5, "Reverted" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskStatus");
        }
    }
}
