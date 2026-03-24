using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpcUaEmulator.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "opcua");

            migrationBuilder.CreateTable(
                name: "emulated_nodes",
                schema: "opcua",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NodeId = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    BrowseName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DataType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CurrentValue = table.Column<string>(type: "text", nullable: true),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emulated_nodes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_emulated_nodes_NodeId",
                schema: "opcua",
                table: "emulated_nodes",
                column: "NodeId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "emulated_nodes",
                schema: "opcua");
        }
    }
}
