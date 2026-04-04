namespace TransporteMaritimo.Core.Models
{
    public class Rol
    {
        public int iRolId { get; set; }

        public required string sNombreRol { get; set; }

        public string? Descripcion { get; set; }

        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();

        public ICollection<RolPermiso> RolPermisos { get; set; } = new List<RolPermiso>();
    }
}
