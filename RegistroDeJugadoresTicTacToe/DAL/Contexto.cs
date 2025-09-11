using Microsoft.EntityFrameworkCore;
using RegistroDeJugadoresTicTacToe.Models;

namespace RegistroDeJugadoresTicTacToe.DAL;

public class Contexto : DbContext
{
    public DbSet<Jugadores> Jugadores { get; set; }
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }
    
}
