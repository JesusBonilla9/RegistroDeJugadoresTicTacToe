using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroDeJugadoresTicTacToe.Migrations
{
    /// <inheritdoc />
    public partial class VictoriasDerrotasEmpates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Partida",
                table: "Jugadores",
                newName: "Victorias");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Victorias",
                table: "Jugadores",
                newName: "Partida");
        }
    }
}
