using System;
using System.Collections.Generic;
using System.Text;

namespace TransporteMaritimo.Core.Models
{
    public class TipoLicencia
    {
        public int TipoLicenciaId { get; set; }

        public required string Nombre { get; set; }

        public ICollection<Licencia>? Licencias { get; set; }
    }
}
