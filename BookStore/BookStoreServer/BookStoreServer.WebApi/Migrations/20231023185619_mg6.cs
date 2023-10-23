using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreServer.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class mg6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OrderNumber",
                table: "OrderStatuses",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatuses_Status_OrderNumber",
                table: "OrderStatuses",
                columns: new[] { "Status", "OrderNumber" },
                unique: true,
                filter: "[OrderNumber] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OrderStatuses_Status_OrderNumber",
                table: "OrderStatuses");

            migrationBuilder.AlterColumn<string>(
                name: "OrderNumber",
                table: "OrderStatuses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
