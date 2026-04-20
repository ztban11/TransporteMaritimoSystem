using System;
using System.Collections.Generic;
using System.Text;

namespace TransporteMaritimo.Core.Models
{
    public class Travesia
    {
        public int TravesiaId { get; set; }

        public int BarcoId { get; set; }

        public string PuertoOrigen { get; set; } = "";

        public string PuertoDestino { get; set; } = "";

        public DateTime FechaSalidaPrevista { get; set; }

        public DateTime FechaLlegadaPrevista { get; set; }

        public string Estado { get; set; } = "";

        public DateTime? FechaCierreReal { get; set; }

        public string? UsuarioCierre { get; set; }
    }
}
