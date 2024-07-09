using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcess.Migrations
{
    /// <inheritdoc />
    public partial class updatePostDeleteUsrAndName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "posts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
