using Microsoft.EntityFrameworkCore;
using RegistroDeJugadoresTicTacToe.DAL;
using RegistroDeJugadoresTicTacToe.Models;
using System.Linq.Expressions;

namespace RegistroDeJugadoresTicTacToe.Services;

public class MovimientosService(IDbContextFactory<Contexto> DbFactory)
{
        public async Task<bool> Guardar(Movimientos movimiento)
        {
            if (!await Existe(movimiento.MovimientoId))
            {
                return await Insertar(movimiento);
            }
            else
            {
                return await Modificar(movimiento);
            }
        }
        public async Task<bool> Existe(int movimientoId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Movimientos.AnyAsync(p => p.MovimientoId == movimientoId);
        }
        private async Task<bool> Insertar(Movimientos movimiento)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Movimientos.Add(movimiento);
            return await contexto.SaveChangesAsync() > 0;
        }
        private async Task<bool> Modificar(Movimientos movimiento)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Update(movimiento);
            return await contexto.SaveChangesAsync() > 0;
        }
        public async Task<Movimientos?> Buscar(int movimientoId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Movimientos
                .FirstOrDefaultAsync(p => p.MovimientoId == movimientoId);
        }
        public async Task<bool> Eliminar(int movimientoId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Movimientos
                .AsNoTracking()
                .Where(p => p.MovimientoId == movimientoId)
                .ExecuteDeleteAsync() > 0;
        }
        public async Task<List<Movimientos>> Listar(Expression<Func<Movimientos, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Movimientos
                .Where(criterio)
                .AsNoTracking()
                .ToListAsync();
        }
}
