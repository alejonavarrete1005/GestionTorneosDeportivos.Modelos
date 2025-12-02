using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionTorneosDeportivos.Modelos
{
    public class Equipo
    {
        [Key] public int Id { get; set; }
        public string Nombre { get; set; }
        public string Ciudad { get; set; }
        

        // Navegacion
        public List<TorneoEquipo> Torneos { get; set; } 
        public List<Jugador> Jugadores { get; set; } 
    }
}
