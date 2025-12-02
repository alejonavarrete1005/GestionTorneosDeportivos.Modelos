using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionTorneosDeportivos.Modelos
{
    public class Jugador
    {
        [Key] public int Id { get; set; }
        public string Nombre { get; set; }
        public int Dorsal { get; set; }
        public string Posicion { get; set; }

        // FK
        public int EquipoId { get; set; }

        //Navegacion
        public Equipo? Equipo { get; set; }
        public List<EventoPartido>? Eventos { get; set; } 
    }
}
