using Microsoft.EntityFrameworkCore.Migrations;

namespace IMDB.WebAPI.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BD_Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    ChaveSenha = table.Column<string>(nullable: true),
                    Senha = table.Column<string>(nullable: true),
                    Deletado = table.Column<bool>(nullable: false),
                    Regra = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BD_Usuarios", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BD_Usuarios");
        }
    }
}
