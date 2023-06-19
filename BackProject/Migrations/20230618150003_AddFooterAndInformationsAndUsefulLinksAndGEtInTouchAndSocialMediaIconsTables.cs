using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackProject.Migrations
{
    public partial class AddFooterAndInformationsAndUsefulLinksAndGEtInTouchAndSocialMediaIconsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FooterId",
                table: "Logos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Footer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Creator = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Footer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GetInTouch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FooterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GetInTouch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GetInTouch_Footer_FooterId",
                        column: x => x.FooterId,
                        principalTable: "Footer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Informations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FooterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Informations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Informations_Footer_FooterId",
                        column: x => x.FooterId,
                        principalTable: "Footer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SocialMediaIcons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FooterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMediaIcons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocialMediaIcons_Footer_FooterId",
                        column: x => x.FooterId,
                        principalTable: "Footer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UsefulLinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FooterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsefulLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsefulLinks_Footer_FooterId",
                        column: x => x.FooterId,
                        principalTable: "Footer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Logos_FooterId",
                table: "Logos",
                column: "FooterId");

            migrationBuilder.CreateIndex(
                name: "IX_GetInTouch_FooterId",
                table: "GetInTouch",
                column: "FooterId");

            migrationBuilder.CreateIndex(
                name: "IX_Informations_FooterId",
                table: "Informations",
                column: "FooterId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialMediaIcons_FooterId",
                table: "SocialMediaIcons",
                column: "FooterId");

            migrationBuilder.CreateIndex(
                name: "IX_UsefulLinks_FooterId",
                table: "UsefulLinks",
                column: "FooterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Logos_Footer_FooterId",
                table: "Logos",
                column: "FooterId",
                principalTable: "Footer",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logos_Footer_FooterId",
                table: "Logos");

            migrationBuilder.DropTable(
                name: "GetInTouch");

            migrationBuilder.DropTable(
                name: "Informations");

            migrationBuilder.DropTable(
                name: "SocialMediaIcons");

            migrationBuilder.DropTable(
                name: "UsefulLinks");

            migrationBuilder.DropTable(
                name: "Footer");

            migrationBuilder.DropIndex(
                name: "IX_Logos_FooterId",
                table: "Logos");

            migrationBuilder.DropColumn(
                name: "FooterId",
                table: "Logos");
        }
    }
}
