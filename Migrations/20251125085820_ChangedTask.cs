using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OPP_back.Migrations
{
    /// <inheritdoc />
    public partial class ChangedTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PosX",
                table: "Tasks",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PosY",
                table: "Tasks",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PosX",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "PosY",
                table: "Tasks");
        }
    }
}
