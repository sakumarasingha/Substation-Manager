using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SM.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class transformer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Substations_SubstationId",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_Assets_SubstationId",
                table: "Assets");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Assets_SubstationId",
                table: "Assets",
                column: "SubstationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Substations_SubstationId",
                table: "Assets",
                column: "SubstationId",
                principalTable: "Substations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
