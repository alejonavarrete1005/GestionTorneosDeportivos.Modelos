using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionTorneosDeportivos.Modelos
{
    public class TorneoEquipo
    {
        [Key] public int Id { get; set; }

        // FK
        public int TorneoId { get; set; }
        public int EquipoId { get; set; }

        //Navegacion
        public Torneo Torneo { get; set; }
        public Equipo Equipo { get; set; }

        // Estadísticas
        public string Grupo { get; set; } // A, B, C, D
        public int Puntos { get; set; }
        public int GolesFavor { get; set; }
        public int GolesContra { get; set; }
        public int Diferencia { get; set; }
    }
}
