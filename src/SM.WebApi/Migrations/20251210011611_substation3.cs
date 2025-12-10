using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SM.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class substation3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Substations_CustomerId",
                table: "Substations",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Substations_Customers_CustomerId",
                table: "Substations",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Substations_Customers_CustomerId",
                table: "Substations");

            migrationBuilder.DropIndex(
                name: "IX_Substations_CustomerId",
                table: "Substations");
        }
    }
}
