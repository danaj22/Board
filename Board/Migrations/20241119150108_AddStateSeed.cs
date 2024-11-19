using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Board.Migrations
{
    /// <inheritdoc />
    public partial class AddStateSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "States", 
                column: "Value", 
                value: "On hold");

            migrationBuilder.InsertData(
                table: "States",
                column: "Value",
                value: "Rejected");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "Value",
                keyValue: "On hold");

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "Value",
                keyValue: "Rejected");
        }
    }
}
