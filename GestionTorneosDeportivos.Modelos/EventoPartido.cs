using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionTorneosDeportivos.Modelos
{
    public class EventoPartido
    {
        [Key] public int Id { get; set; }
        public string Tipo { get; set; } // Gol, tarjeta amarilla, tarjeta roja
        public int Minuto { get; set; }

        //Fk
        public int PartidoId { get; set; }
        public int JugadorId { get; set; }

        //Navegacion
        public Partido? Partido { get; set; }
               
        public Jugador? Jugador { get; set; }

        
    }
}
