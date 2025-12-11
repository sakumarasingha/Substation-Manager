using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SM.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class assetType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "AssetTypes");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "AssetTypes",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "AssetTypes");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AssetTypes",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }
    }
}
