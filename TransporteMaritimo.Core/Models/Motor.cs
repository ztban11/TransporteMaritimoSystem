using System;
using System.Collections.Generic;
using System.Text;

namespace TransporteMaritimo.Core.Models
{
    public class Motor
    {
        public int MotorId { get; set; }

        public int BarcoId { get; set; }

        public string Modelo { get; set; } = "";

        public int PotenciaHP { get; set; }

        public int HorasUso { get; set; }
    }

}
