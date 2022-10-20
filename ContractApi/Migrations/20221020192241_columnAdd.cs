using Microsoft.EntityFrameworkCore.Migrations;

namespace ContractApi.Migrations
{
    public partial class columnAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirmName",
                table: "Contracts",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirmName",
                table: "Contracts");
        }
    }
}
