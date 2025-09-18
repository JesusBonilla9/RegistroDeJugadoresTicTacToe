using Microsoft.EntityFrameworkCore;
using RegistroDeJugadoresTicTacToe.Models;

namespace RegistroDeJugadoresTicTacToe.DAL;

public class Contexto : DbContext
{
    public DbSet<Jugadores> Jugadores { get; set; }
    public DbSet<Partidas> Partidas { get; set; }
    public DbSet<Movimientos> Movimientos { get; set; }
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Partidas>()
        .HasOne(p => p.Jugador1)
        .WithMany()
        .HasForeignKey(p => p.Jugador1Id)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Partidas>()
        .HasOne(p => p.Jugador2)
        .WithMany()
        .HasForeignKey(p => p.Jugador2Id)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Partidas>()
        .HasOne(p => p.Ganador)
        .WithMany()
        .HasForeignKey(p => p.GanadorId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Partidas>()
        .HasOne(p => p.TurnoJugador)
        .WithMany()
        .HasForeignKey(p => p.TurnoJugadorId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Movimientos>(entity =>
        {
            entity.HasOne(m => m.Jugador)
                  .WithMany(j => j.Movimientos)
                  .HasForeignKey(m => m.JugadorId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(m => m.Partida)
                  .WithMany()
                  .HasForeignKey(m => m.PartidaId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
        base.OnModelCreating(modelBuilder);
    }
}
