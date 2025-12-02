using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionTorneosDeportivos.Modelos.Dtos
{
    public class TarjetaEventoDTO
    {
        public int JugadorId { get; set; }
        public string Tipo { get; set; } // "Amarilla" o "Roja"
        public int Minuto { get; set; }
    }
}
