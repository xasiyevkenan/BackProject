using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackProject.Migrations
{
    public partial class AddHeaderAndNavigationsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Header",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Header", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Header_Logos_LogoId",
                        column: x => x.LogoId,
                        principalTable: "Logos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Navigations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMain = table.Column<bool>(type: "bit", nullable: false),
                    ParentNavigationId = table.Column<int>(type: "int", nullable: true),
                    HeaderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Navigations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Navigations_Header_HeaderId",
                        column: x => x.HeaderId,
                        principalTable: "Header",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Navigations_Navigations_ParentNavigationId",
                        column: x => x.ParentNavigationId,
                        principalTable: "Navigations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Header_LogoId",
                table: "Header",
                column: "LogoId");

            migrationBuilder.CreateIndex(
                name: "IX_Navigations_HeaderId",
                table: "Navigations",
                column: "HeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Navigations_ParentNavigationId",
                table: "Navigations",
                column: "ParentNavigationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Navigations");

            migrationBuilder.DropTable(
                name: "Header");
        }
    }
}
