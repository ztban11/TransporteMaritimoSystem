using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TransporteMaritimo.Core.Models;

namespace TransporteMaritimo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LicenciasController : ControllerBase
    {
        private readonly IConfiguration _config;

        public LicenciasController(IConfiguration config)
        {
            _config = config;
        }

        // GET api/licencias
        [HttpGet]
        public IActionResult Get()
        {
            var licencias = new List<Licencia>();

            using var conn = new SqlConnection(
                _config.GetConnectionString("DefaultConnection")
            );

            conn.Open();

            var cmd = new SqlCommand(
                "SELECT * FROM Licencias",
                conn
            );

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                licencias.Add(new Licencia
                {
                    LicenciaId = (int)reader["LicenciaId"],
                    PersonalId = (int)reader["PersonalId"],
                    TipoLicencia = reader["TipoLicencia"]?.ToString() ?? "",
                    FechaExpiracion = (DateTime)reader["FechaExpiracion"]
                });
            }

            return Ok(licencias);
        }

        // POST api/licencias
        [HttpPost]
        public IActionResult Post([FromBody] Licencia licencia)
        {
            if (licencia == null)
                return BadRequest();

            using var conn = new SqlConnection(
                _config.GetConnectionString("DefaultConnection")
            );

            conn.Open();

            var cmd = new SqlCommand(@"
                INSERT INTO Licencias
                (PersonalId, TipoLicencia, FechaExpiracion)
                VALUES
                (@PersonalId, @TipoLicencia, @FechaExpiracion)
            ", conn);

            cmd.Parameters.AddWithValue("@PersonalId", licencia.PersonalId);
            cmd.Parameters.AddWithValue("@TipoLicencia", licencia.TipoLicencia);
            cmd.Parameters.AddWithValue("@FechaExpiracion", licencia.FechaExpiracion);

            cmd.ExecuteNonQuery();

            return Ok(licencia);
        }
    }
}