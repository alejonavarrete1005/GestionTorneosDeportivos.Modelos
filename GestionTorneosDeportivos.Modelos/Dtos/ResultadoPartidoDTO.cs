using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionTorneosDeportivos.Modelos.Dtos
{
    public  class ResultadoPartidoDTO
    {
        public int PartidoId { get; set; }

        // Resultado final del partido
        public int GolesLocal { get; set; }
        public int GolesVisitante { get; set; }

        // Eventos del partido
        public List<GolEventoDTO> Goles { get; set; }
        public List<TarjetaEventoDTO> Tarjetas { get; set; }
    }
}
