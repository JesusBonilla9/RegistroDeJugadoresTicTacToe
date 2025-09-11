using System.ComponentModel.DataAnnotations;

namespace RegistroDeJugadoresTicTacToe.Models;

public class Jugadores
{
    [Key]
    public int JugadorId { get; set; }
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    public string Nombre { get; set; } = string.Empty;
    [Required(ErrorMessage = "El numero de partidas es obligatorio. ")]
    [Range(0, int.MaxValue, ErrorMessage = "Debe ser un numero valido. ")]
    public int Partida { get; set; }
}