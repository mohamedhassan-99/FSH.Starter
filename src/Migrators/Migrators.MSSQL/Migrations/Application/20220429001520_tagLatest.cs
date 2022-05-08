using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    public partial class tagLatest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetTag_Assets_AssetsId",
                schema: "Catalog",
                table: "AssetTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssetTag",
                schema: "Catalog",
                table: "AssetTag");

            migrationBuilder.DropIndex(
                name: "IX_AssetTag_TagsId",
                schema: "Catalog",
                table: "AssetTag");

            migrationBuilder.RenameColumn(
                name: "AssetsId",
                schema: "Catalog",
                table: "AssetTag",
                newName: "TagsIds");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "Catalog",
                table: "Departments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "Catalog",
                table: "Departments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                schema: "Catalog",
                table: "Departments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                schema: "Catalog",
                table: "Departments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifiedBy",
                schema: "Catalog",
                table: "Departments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "Catalog",
                table: "Departments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssetTag",
                schema: "Catalog",
                table: "AssetTag",
                columns: new[] { "TagsId", "TagsIds" });

            migrationBuilder.CreateIndex(
                name: "IX_AssetTag_TagsIds",
                schema: "Catalog",
                table: "AssetTag",
                column: "TagsIds");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetTag_Assets_TagsIds",
                schema: "Catalog",
                table: "AssetTag",
                column: "TagsIds",
                principalSchema: "Catalog",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetTag_Assets_TagsIds",
                schema: "Catalog",
                table: "AssetTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssetTag",
                schema: "Catalog",
                table: "AssetTag");

            migrationBuilder.DropIndex(
                name: "IX_AssetTag_TagsIds",
                schema: "Catalog",
                table: "AssetTag");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Catalog",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Catalog",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                schema: "Catalog",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                schema: "Catalog",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "Catalog",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "Catalog",
                table: "Departments");

            migrationBuilder.RenameColumn(
                name: "TagsIds",
                schema: "Catalog",
                table: "AssetTag",
                newName: "AssetsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssetTag",
                schema: "Catalog",
                table: "AssetTag",
                columns: new[] { "AssetsId", "TagsId" });

            migrationBuilder.CreateIndex(
                name: "IX_AssetTag_TagsId",
                schema: "Catalog",
                table: "AssetTag",
                column: "TagsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetTag_Assets_AssetsId",
                schema: "Catalog",
                table: "AssetTag",
                column: "AssetsId",
                principalSchema: "Catalog",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
