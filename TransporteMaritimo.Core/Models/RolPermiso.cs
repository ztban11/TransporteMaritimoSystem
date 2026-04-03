namespace TransporteMaritimo.Core.Models
{
    public class RolPermiso
    {
        public int iRolPermisoId { get; set; }
        public int iRolId { get; set; }
        public int iPermisoId { get; set; }
        public Rol? Rol { get; set; }
        public Permiso? Permiso { get; set; }
    }
}
