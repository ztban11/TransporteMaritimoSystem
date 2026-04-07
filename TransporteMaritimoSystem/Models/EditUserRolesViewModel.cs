namespace TransporteMaritimoSystem.Models
{
    public class EditUserRolesViewModel
    {
        public int iUsuarioId { get; set; }

        public required string sNombre { get; set; }

        public List<RoleCheckbox> Roles { get; set; } = new();
    }
    public class RoleCheckbox
    {
        public int iRolId { get; set; }

        public required string sNombreRol { get; set; }

        public bool bSeleccionada { get; set; }
    }


}
