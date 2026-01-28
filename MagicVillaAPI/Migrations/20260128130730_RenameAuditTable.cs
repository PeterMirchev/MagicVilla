using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class RenameAuditTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AuditRecord",
                table: "AuditRecord");

            migrationBuilder.RenameTable(
                name: "AuditRecord",
                newName: "AuditRecords");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuditRecords",
                table: "AuditRecords",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AuditRecords",
                table: "AuditRecords");

            migrationBuilder.RenameTable(
                name: "AuditRecords",
                newName: "AuditRecord");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuditRecord",
                table: "AuditRecord",
                column: "Id");
        }
    }
}
