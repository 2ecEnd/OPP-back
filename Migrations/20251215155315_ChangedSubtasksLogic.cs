using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OPP_back.Migrations
{
    /// <inheritdoc />
    public partial class ChangedSubtasksLogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Tasks_SuperTaskId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_SuperTaskId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "SuperTaskId",
                table: "Tasks");

            migrationBuilder.CreateTable(
                name: "TaskTask",
                columns: table => new
                {
                    SubTasksId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTask", x => new { x.SubTasksId, x.TaskId });
                    table.ForeignKey(
                        name: "FK_TaskTask_Tasks_SubTasksId",
                        column: x => x.SubTasksId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskTask_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskTask_TaskId",
                table: "TaskTask",
                column: "TaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskTask");

            migrationBuilder.AddColumn<Guid>(
                name: "SuperTaskId",
                table: "Tasks",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_SuperTaskId",
                table: "Tasks",
                column: "SuperTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Tasks_SuperTaskId",
                table: "Tasks",
                column: "SuperTaskId",
                principalTable: "Tasks",
                principalColumn: "Id");
        }
    }
}
