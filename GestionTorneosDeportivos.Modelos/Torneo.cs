using System.ComponentModel.DataAnnotations;
namespace GestionTorneosDeportivos.Modelos

{
    public class Torneo
    {
        [Key] public int Id { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; } // Liga, Copa, Mixto
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Estado { get; set; } // Pendiente, EnCurso, Finalizado

        // Navegacion
        public List<TorneoEquipo> TorneoEquipos { get; set; } 
        public List<Partido> Partidos { get; set; } 
    }
}
