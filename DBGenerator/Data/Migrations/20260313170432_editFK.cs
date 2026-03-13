using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBGenerator.Data.Migrations
{
    /// <inheritdoc />
    public partial class editFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForeignKey_Tables_TableId",
                table: "ForeignKey");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ForeignKey",
                table: "ForeignKey");

            migrationBuilder.RenameTable(
                name: "ForeignKey",
                newName: "ForeignKeys");

            migrationBuilder.RenameIndex(
                name: "IX_ForeignKey_TableId",
                table: "ForeignKeys",
                newName: "IX_ForeignKeys_TableId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ForeignKeys",
                table: "ForeignKeys",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ForeignKeys_Tables_TableId",
                table: "ForeignKeys",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForeignKeys_Tables_TableId",
                table: "ForeignKeys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ForeignKeys",
                table: "ForeignKeys");

            migrationBuilder.RenameTable(
                name: "ForeignKeys",
                newName: "ForeignKey");

            migrationBuilder.RenameIndex(
                name: "IX_ForeignKeys_TableId",
                table: "ForeignKey",
                newName: "IX_ForeignKey_TableId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ForeignKey",
                table: "ForeignKey",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ForeignKey_Tables_TableId",
                table: "ForeignKey",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
