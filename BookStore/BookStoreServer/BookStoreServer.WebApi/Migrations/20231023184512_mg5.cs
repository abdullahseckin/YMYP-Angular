using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreServer.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class mg5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "OrderStatuses");

            migrationBuilder.AddColumn<string>(
                name: "OrderNumber",
                table: "OrderStatuses",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderNumber",
                table: "OrderStatuses");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "OrderStatuses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
