using Microsoft.EntityFrameworkCore;
using RegistroDeJugadoresTicTacToe.DAL;
using RegistroDeJugadoresTicTacToe.Models;
using System.Linq.Expressions;

namespace RegistroDeJugadoresTicTacToe.Services;

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

    public async Task ActualizarEstadisticasFinPartida(int jugador1Id, int jugador2Id, int? ganadorId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var j1 = await contexto.Jugadores.FindAsync(jugador1Id);
        var j2 = await contexto.Jugadores.FindAsync(jugador2Id);
        if (j1 is null || j2 is null) return;

        if (ganadorId == null)
        {
            j1.Empates++; j2.Empates++;
        }
        else if (ganadorId == jugador1Id)
        {
            j1.Victorias++; j2.Derrotas++;
        }
        else if (ganadorId == jugador2Id)
        {
            j2.Victorias++; j1.Derrotas++;
        }

        await contexto.SaveChangesAsync();
    }
    public async Task RevertirEstadisticasPartida(int jugador1Id, int jugador2Id, int? ganadorId)
    {
        await using var ctx = await DbFactory.CreateDbContextAsync();
        var j1 = await ctx.Jugadores.FindAsync(jugador1Id);
        var j2 = await ctx.Jugadores.FindAsync(jugador2Id);
        if (j1 is null || j2 is null) return;

        if (ganadorId == null)
        {
            if (j1.Empates > 0) j1.Empates--;
            if (j2.Empates > 0) j2.Empates--;
        }
        else if (ganadorId == jugador1Id)
        {
            if (j1.Victorias > 0) j1.Victorias--;
            if (j2.Derrotas > 0) j2.Derrotas--;
        }
        else if (ganadorId == jugador2Id)
        {
            if (j2.Victorias > 0) j2.Victorias--;
            if (j1.Derrotas > 0) j1.Derrotas--;
        }

        await ctx.SaveChangesAsync();
    }


}

