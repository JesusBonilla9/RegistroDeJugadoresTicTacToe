
using RegistroDeJugadoresTicTacToe.DTOs;

namespace RegistroDeJugadoresTicTacToe.Services;

public interface IJugadoresApiService
{
    Task<Resource<List<JugadorResponse>>> GetJugadoresAsync();
    Task<Resource<JugadorResponse>> GetJugadoresAsync(int JugadorId);
    Task<Resource<JugadorResponse>> PostJugadores(string nombre, string email);
    Task<Resource<JugadorResponse>> PutJugador(int JugadorId, string nombre, string email);
}
