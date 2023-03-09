using Microsoft.EntityFrameworkCore.Migrations;

namespace XenonHealth.Task.Migrations
{
    public partial class newcolumnimageextension : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageExtension",
                table: "Images",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageExtension",
                table: "Images");
        }
    }
}
