using System;
using System.Collections.Generic;
using System.Text;

namespace TransporteMaritimo.Core.Models
{
    public class UsuarioRol
    {
        public int UsuarioId { get; set; }
        public int RolId { get; set; }
        public Usuario? Usuario { get; set; }
        public Rol? Rol { get; set; }
    }
}
