using System.ComponentModel.DataAnnotations;

namespace TransporteMaritimo.Core.Models
{
    public class Permiso
    {
        [Key]
        public int iPermisoId { get; set; }

        public required string sNombrePermiso { get; set; }

        public string? sDescripcion { get; set; }

        public ICollection<RolPermiso> RolPermisos { get; set; } = new List<RolPermiso>();
    }
}
