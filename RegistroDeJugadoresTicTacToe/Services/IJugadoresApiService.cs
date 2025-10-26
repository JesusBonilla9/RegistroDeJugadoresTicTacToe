using RegistroDeJugadoresTicTacToe.Shared;
using RegistroDeJugadoresTicTacToe.Shared.DTOs;

namespace RegistroDeJugadoresTicTacToe.Services
{
    public interface IJugadoresApiService
    {
        Task<Resource<List<JugadorResponse>>> GetJugadoresAsync();
        Task<Resource<JugadorResponse>> GetJugadorAsync(int jugadorId);
        Task<Resource<JugadorResponse>> PostJugador(string nombres, string email);
    }
}
