using System;
using System.Collections.Generic;
using System.Text;

namespace TransporteMaritimo.Core.Models
{
    public class Personal
    {
        public int PersonalId { get; set; }

        public required string NombreCompleto { get; set; }

        public required string Identificacion { get; set; }

        public required string RolPrimario { get; set; }

        public DateTime FechaContratacion { get; set; }

        public bool Activo { get; set; }
    }
}
