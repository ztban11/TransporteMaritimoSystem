using System;
using System.Collections.Generic;
using System.Text;

namespace TransporteMaritimo.Core.Models
{
    public class OrdenServicio
    {
        public int OrdenId { get; set; }

        public int BarcoId { get; set; }

        public string TipoMantenimiento { get; set; } = "";

        public string Prioridad { get; set; } = "";

        public string Descripcion { get; set; } = "";

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaLimite { get; set; }

        public string Estado { get; set; } = "";

        public string? InformeCierre { get; set; }

        public DateTime? FechaCierreReal { get; set; }

        public string? UsuarioCierre { get; set; }

        public string? NombreBarco { get; set; }
    }
}
