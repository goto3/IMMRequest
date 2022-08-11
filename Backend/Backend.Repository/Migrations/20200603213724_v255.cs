using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Repository.Migrations
{
    public partial class v255 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "MultipleValues",
                table: "AdditionalFields",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MultipleValues",
                table: "AdditionalFields");
        }
    }
}
