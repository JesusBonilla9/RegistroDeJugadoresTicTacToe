using RegistroDeJugadoresTicTacToe.Shared;
using RegistroDeJugadoresTicTacToe.Shared.DTOs;

namespace RegistroDeJugadoresTicTacToe.Services
{
    public interface IMovimientosApiService
    {
        Task<Resource<List<MovimientoResponse>>> GetMovimientosAsync(int partidaId);
        Task<Resource<MovimientoResponse>> PostMovimiento(int PartidaId, string Jugador, int PosicionFila, int PosicionColumna);
    }
}
