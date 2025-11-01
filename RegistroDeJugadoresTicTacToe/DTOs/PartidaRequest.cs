using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroDeJugadoresTicTacToe.DTOs;

public record PartidaRequest(
    int Jugador1Id,
    int? Jugador2Id
);
