using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskManager_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class taskPriority2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskPriorities",
                columns: table => new
                {
                    TaskPriorityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskPriorityName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskPriorities", x => x.TaskPriorityId);
                });

            migrationBuilder.InsertData(
                table: "TaskPriorities",
                columns: new[] { "TaskPriorityId", "TaskPriorityName" },
                values: new object[,]
                {
                    { 1, "Urgent" },
                    { 2, "Normal" },
                    { 3, "Below Normal" },
                    { 4, "Low" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskPriorities");
        }
    }
}
