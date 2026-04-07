namespace TransporteMaritimo.Core.Models
{
    public class Usuario
    {
        public int iUsuarioId { get; set; }

        public required string sNombre { get; set; }

        public required string sEmail { get; set; }

        public required string sPasswordHash { get; set; }

        public bool bActivo { get; set; }

        public int? iIntentosFallidos { get; set; }

        public DateTime? dtBloqueadoHasta { get; set; }

        //public int UsuarioId { get; set; }
        
        //public required Usuario uUsuario { get; set; }

        public ICollection<UsuarioRol> UsuarioRoles { get; set; } = new List<UsuarioRol>();
    }
}
