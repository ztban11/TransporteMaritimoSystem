using System;
using System.Collections.Generic;
using System.Text;

namespace TransporteMaritimo.Core.Models
{
    public class AsignacionTripulacion
    {
        public int AsignacionId { get; set; }

        public int PersonalId { get; set; }

        public int BarcoId { get; set; }

        public required string Cargo { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }
    }
}
