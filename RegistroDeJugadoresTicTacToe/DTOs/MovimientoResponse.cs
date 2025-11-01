using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroDeJugadoresTicTacToe.DTOs;

public record MovimientoResponse(
    int PartidaId,
    string Jugador,
    int PosicionFila,
    int PosicionColumna
);
