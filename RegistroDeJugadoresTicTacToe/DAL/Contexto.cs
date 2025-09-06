using Microsoft.EntityFrameworkCore;
using RegistroDeJugadoresTicTacToe.Models;

namespace RegistroDeJugadoresTicTacToe.DAL
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }
        public DbSet<Jugadores> Jugadores { get; set; }
    }
}
