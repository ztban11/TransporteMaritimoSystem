using System;
using System.Collections.Generic;
using System.Text;

namespace TransporteMaritimo.Core.Models
{
    public class Licencia
    {
        public int LicenciaId { get; set; }

        public int PersonalId { get; set; }

        public required string TipoLicencia { get; set; }

        public DateTime FechaExpiracion { get; set; }
    }
}
