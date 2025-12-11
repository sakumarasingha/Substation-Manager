using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SM.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class transformer1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transformers_Substations_SubstationId",
                table: "Transformers");

            migrationBuilder.DropIndex(
                name: "IX_Transformers_SubstationId",
                table: "Transformers");

            migrationBuilder.DropColumn(
                name: "SubstationId",
                table: "Transformers");

            migrationBuilder.AlterColumn<string>(
                name: "SerialNumber",
                table: "Transformers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_AssetTypeId",
                table: "Assets",
                column: "AssetTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_SubstationId",
                table: "Assets",
                column: "SubstationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_AssetTypes_AssetTypeId",
                table: "Assets",
                column: "AssetTypeId",
                principalTable: "AssetTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Substations_SubstationId",
                table: "Assets",
                column: "SubstationId",
                principalTable: "Substations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_AssetTypes_AssetTypeId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Substations_SubstationId",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_Assets_AssetTypeId",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_Assets_SubstationId",
                table: "Assets");

            migrationBuilder.AlterColumn<string>(
                name: "SerialNumber",
                table: "Transformers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<Guid>(
                name: "SubstationId",
                table: "Transformers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transformers_SubstationId",
                table: "Transformers",
                column: "SubstationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transformers_Substations_SubstationId",
                table: "Transformers",
                column: "SubstationId",
                principalTable: "Substations",
                principalColumn: "Id");
        }
    }
}
