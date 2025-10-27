using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroDeJugadoresTicTacToe.Shared.DTOs;

public record MovimientoResponse(
     int MovimientoId,
     string Jugador, 
     int PosicionFila, 
     int PosicionColumna 
);
