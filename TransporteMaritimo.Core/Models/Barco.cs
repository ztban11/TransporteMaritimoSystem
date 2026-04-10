using System;
using System.Collections.Generic;
using System.Text;

namespace TransporteMaritimo.Core.Models
{
    public class Barco
    {
        public int BarcoId { get; set; }

        public required string NombreBarco { get; set; }

        public required string Tipo { get; set; }
    }
}
