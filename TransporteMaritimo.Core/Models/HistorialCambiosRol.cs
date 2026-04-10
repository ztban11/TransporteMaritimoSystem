using System;
using System.Collections.Generic;
using System.Text;

namespace TransporteMaritimo.Core.Models
{
    public class HistorialCambiosRol
    {
        public int CambioRolId { get; set; }

        public int UsuarioId { get; set; }

        public int? RolAnteriorId { get; set; }

        public int? RolNuevoId { get; set; }

        public int ModificadoPorUsuarioId { get; set; }

        public DateTime FechaCambio { get; set; }
    }
}
