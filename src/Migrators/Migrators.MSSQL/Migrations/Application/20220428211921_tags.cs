using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    public partial class tags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Rate",
                schema: "Catalog",
                table: "Assets",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "Barcode",
                schema: "Catalog",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                schema: "Catalog",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                schema: "Catalog",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                schema: "Catalog",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                schema: "Catalog",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QrCode",
                schema: "Catalog",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Summary",
                schema: "Catalog",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Vendor",
                schema: "Catalog",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AssetTag",
                schema: "Catalog",
                columns: table => new
                {
                    AssetsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetTag", x => new { x.AssetsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_AssetTag_Assets_AssetsId",
                        column: x => x.AssetsId,
                        principalSchema: "Catalog",
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalSchema: "Catalog",
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssetTag_TagsId",
                schema: "Catalog",
                table: "AssetTag",
                column: "TagsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetTag",
                schema: "Catalog");

            migrationBuilder.DropColumn(
                name: "Barcode",
                schema: "Catalog",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Latitude",
                schema: "Catalog",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Location",
                schema: "Catalog",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Longitude",
                schema: "Catalog",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Model",
                schema: "Catalog",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "QrCode",
                schema: "Catalog",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Summary",
                schema: "Catalog",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Vendor",
                schema: "Catalog",
                table: "Assets");

            migrationBuilder.AlterColumn<decimal>(
                name: "Rate",
                schema: "Catalog",
                table: "Assets",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);
        }
    }
}
