namespace TransporteMaritimo.Core.Models
{
    public class Usuario
    {
        public int iUsuarioId { get; set; }

        public required string sNombre { get; set; }

        public required string sEmail { get; set; }

        public required string sPasswordHash { get; set; }

        public int iRolId { get; set; }

        public bool bActivo { get; set; }

        public Rol? Rol { get; set; }
    }
}
