namespace TransporteMaritimo.API.Models
{
    public class LoginRequest
    {
        public required string sEmail { get; set; }

        public required string sPassword { get; set; }
    }
}
