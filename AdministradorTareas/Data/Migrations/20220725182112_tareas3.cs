using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdministradorTareas.Data.Migrations
{
    public partial class tareas3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "creador",
                table: "tareas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "creador",
                table: "tareas");
        }
    }
}
