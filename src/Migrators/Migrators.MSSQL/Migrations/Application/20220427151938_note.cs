using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    public partial class note : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Brands_BrandId",
                schema: "Catalog",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Categories_CategoryId",
                schema: "Catalog",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Projects_ProjectId",
                schema: "Catalog",
                table: "Assets");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                schema: "Catalog",
                table: "Assets",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                schema: "Catalog",
                table: "Assets",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "BrandId",
                schema: "Catalog",
                table: "Assets",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                schema: "Catalog",
                table: "Assets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Notes",
                schema: "Catalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NoteContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssetId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_Assets_AssetId",
                        column: x => x.AssetId,
                        principalSchema: "Catalog",
                        principalTable: "Assets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                schema: "Catalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assets_DepartmentId",
                schema: "Catalog",
                table: "Assets",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_AssetId",
                schema: "Catalog",
                table: "Notes",
                column: "AssetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Brands_BrandId",
                schema: "Catalog",
                table: "Assets",
                column: "BrandId",
                principalSchema: "Catalog",
                principalTable: "Brands",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Categories_CategoryId",
                schema: "Catalog",
                table: "Assets",
                column: "CategoryId",
                principalSchema: "Catalog",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Departments_DepartmentId",
                schema: "Catalog",
                table: "Assets",
                column: "DepartmentId",
                principalSchema: "Catalog",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Projects_ProjectId",
                schema: "Catalog",
                table: "Assets",
                column: "ProjectId",
                principalSchema: "Catalog",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Brands_BrandId",
                schema: "Catalog",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Categories_CategoryId",
                schema: "Catalog",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Departments_DepartmentId",
                schema: "Catalog",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Projects_ProjectId",
                schema: "Catalog",
                table: "Assets");

            migrationBuilder.DropTable(
                name: "Notes",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "Tags",
                schema: "Catalog");

            migrationBuilder.DropIndex(
                name: "IX_Assets_DepartmentId",
                schema: "Catalog",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                schema: "Catalog",
                table: "Assets");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                schema: "Catalog",
                table: "Assets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                schema: "Catalog",
                table: "Assets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "BrandId",
                schema: "Catalog",
                table: "Assets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Brands_BrandId",
                schema: "Catalog",
                table: "Assets",
                column: "BrandId",
                principalSchema: "Catalog",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Categories_CategoryId",
                schema: "Catalog",
                table: "Assets",
                column: "CategoryId",
                principalSchema: "Catalog",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Projects_ProjectId",
                schema: "Catalog",
                table: "Assets",
                column: "ProjectId",
                principalSchema: "Catalog",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
