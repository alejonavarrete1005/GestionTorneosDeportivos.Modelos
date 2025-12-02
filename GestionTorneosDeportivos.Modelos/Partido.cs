using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionTorneosDeportivos.Modelos
{
    public class Partido
    {
        [Key] public int Id { get; set; }

        public int TorneoId { get; set; }
        public Torneo? Torneo { get; set; }

        public string Fase { get; set; } // Grupos, Cuartos, Semis, Final
        public DateTime Fecha { get; set; }
        public bool Jugado { get; set; } 

        // Fk
        public int EquipoLocalId { get; set; }
        public Equipo? EquipoLocal { get; set; }

        public int EquipoVisitanteId { get; set; }
        public Equipo? EquipoVisitante { get; set; }

        // Resultado
        public int GolesLocal { get; set; }
        public int GolesVisitante { get; set; }

        // Ganador en eliminación directa
        public int? GanadorId { get; set; }
        public Equipo? Ganador { get; set; }

        //Navegacion
        public List<EventoPartido>? Eventos { get; set; } 
    }
}
