namespace TransporteMaritimoSystem.Models
{
    public class HistorialRolViewModel
    {
        public required string Usuario { get; set; }

        public required string RolAnterior { get; set; }

        public required string RolNuevo { get; set; }

        public required string Administrador { get; set; }

        public DateTime FechaCambio { get; set; }
    }
}
