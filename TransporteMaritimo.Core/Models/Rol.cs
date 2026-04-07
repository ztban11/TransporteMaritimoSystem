namespace TransporteMaritimo.Core.Models
{
    public class Rol
    {
        public int iRolId { get; set; }

        public required string sNombreRol { get; set; }

        public string? Descripcion { get; set; }
        public ICollection<UsuarioRol> UsuarioRoles { get; set; } = new List<UsuarioRol>();
        public ICollection<RolPermiso> RolPermisos { get; set; } = new List<RolPermiso>();
    }
}
