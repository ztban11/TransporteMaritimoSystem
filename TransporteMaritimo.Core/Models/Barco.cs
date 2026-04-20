using System;
using System.Collections.Generic;
using System.Text;

namespace TransporteMaritimo.Core.Models
{
    public class Barco
    {
        public int BarcoId { get; set; }

        public string NombreBarco { get; set; } = "";

        public string Tipo { get; set; } = "";

        public string Matricula { get; set; } = "";

        public decimal CapacidadCarga { get; set; }

        public string PuertoBase { get; set; } = "";

        public bool Activo { get; set; }

        // Motor
        public string ModeloMotor { get; set; } = "";

        public int PotenciaHP { get; set; }

        public int HorasUso { get; set; }
    }
}
