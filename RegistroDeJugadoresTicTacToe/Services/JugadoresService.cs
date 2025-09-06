using Microsoft.EntityFrameworkCore;
using RegistroDeJugadoresTicTacToe.DAL;
using RegistroDeJugadoresTicTacToe.Models;
using System.Linq.Expressions;

namespace RegistroDeJugadoresTicTacToe.Services
{
    public class JugadoresService(IDbContextFactory<Contexto> DbFactory)
    {
        public async Task<bool> Guardar(Jugadores Jugador)
        {
            if (!await Existe(Jugador.JugadorId))
            {
                return await Insertar(Jugador);
            }
            else
            {
                return await Modificar(Jugador);
            }
        }
        public async Task<bool> Existe(int JugadorId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Jugadores.AnyAsync(p => p.JugadorId == JugadorId);
        }
        private async Task<bool> ExisteNombre(string nombre, int jugadorId = 0)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Jugadores.AnyAsync(p => p.Nombre == nombre && p.JugadorId != jugadorId);
        }
        private async Task<bool> Insertar(Jugadores Jugador)
        {
            if (await ExisteNombre(Jugador.Nombre))
            {
                throw new Exception("Ya existe un jugador con ese nombre.");
            }
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Jugadores.Add(Jugador);
            return await contexto.SaveChangesAsync() > 0;
        }
        private async Task<bool> Modificar(Jugadores Jugador)
        {
            if (await ExisteNombre(Jugador.Nombre, Jugador.JugadorId))
            {
                throw new Exception("Ya existe un jugador con ese nombre.");
            }
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Update(Jugador);
            return await contexto.SaveChangesAsync() > 0;
        }
        public async Task<Jugadores?> Buscar(int JugadorId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Jugadores.FirstOrDefaultAsync(p => p.JugadorId == JugadorId);

        }
        public async Task<bool> Eliminar(int JugadorId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Jugadores.AsNoTracking().Where(p => p.JugadorId == JugadorId).ExecuteDeleteAsync() > 0;
        }
        public async Task<List<Jugadores>> Listar(Expression<Func<Jugadores, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Jugadores.Where(criterio).AsNoTracking().ToListAsync();
        }
    }
}

