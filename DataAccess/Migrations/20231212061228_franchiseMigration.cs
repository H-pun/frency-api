using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Frency.DataAccess.Migrations
{
    public partial class franchiseMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "franchise",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    address = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    category = table.Column<string>(type: "text", nullable: true),
                    whatsapp_number = table.Column<string>(type: "text", nullable: true),
                    file_path = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_franchise", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "franchise_bundle",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    id_franchise = table.Column<Guid>(type: "uuid", nullable: false),
                    franchise_type = table.Column<string>(type: "text", nullable: true),
                    facility = table.Column<string>(type: "text", nullable: true),
                    price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_franchise_bundle", x => x.id);
                    table.ForeignKey(
                        name: "fk_franchise_bundle_franchise_id_franchise",
                        column: x => x.id_franchise,
                        principalTable: "franchise",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_franchise_bundle_id_franchise",
                table: "franchise_bundle",
                column: "id_franchise");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "franchise_bundle");

            migrationBuilder.DropTable(
                name: "franchise");
        }
    }
}
