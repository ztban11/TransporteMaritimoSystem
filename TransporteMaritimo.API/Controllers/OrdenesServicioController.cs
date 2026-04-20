using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TransporteMaritimo.Core.Models;

namespace TransporteMaritimo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdenesServicioController : ControllerBase
    {
        private readonly IConfiguration _config;

        public OrdenesServicioController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var ordenes = new List<OrdenServicio>();

            using var conn = new SqlConnection(
                _config.GetConnectionString("DefaultConnection"));

            conn.Open();

            var cmd = new SqlCommand(@"
                SELECT 
o.OrdenId,
o.BarcoId,
b.NombreBarco,
o.TipoMantenimiento,
o.Prioridad,
o.Descripcion,
o.FechaCreacion,
o.FechaLimite,
o.Estado,
o.InformeCierre,
o.FechaCierreReal,
o.UsuarioCierre
FROM OrdenesServicio o
INNER JOIN Barcos b
ON o.BarcoId = b.BarcoId", conn);

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ordenes.Add(new OrdenServicio
                {
                    OrdenId = (int)reader["OrdenId"],
                    BarcoId = (int)reader["BarcoId"],
                    NombreBarco = reader["NombreBarco"].ToString() ?? "",
                    TipoMantenimiento = reader["TipoMantenimiento"].ToString() ?? "",
                    Prioridad = reader["Prioridad"].ToString() ?? "",
                    Descripcion = reader["Descripcion"].ToString() ?? "",
                    FechaCreacion = (DateTime)reader["FechaCreacion"],
                    FechaLimite = (DateTime)reader["FechaLimite"],
                    Estado = reader["Estado"].ToString() ?? "",
                    InformeCierre = reader["InformeCierre"]?.ToString(),
                    FechaCierreReal = reader["FechaCierreReal"] as DateTime?,
                    UsuarioCierre = reader["UsuarioCierre"]?.ToString()
                });
            }

            return Ok(ordenes);
        }

        [HttpPost]
        public IActionResult Post(OrdenServicio orden)
        {
            using var conn = new SqlConnection(
                _config.GetConnectionString("DefaultConnection"));

            conn.Open();

            var cmd = new SqlCommand(@"
                INSERT INTO OrdenesServicio
                (BarcoId, TipoMantenimiento, Prioridad, Descripcion, FechaCreacion, FechaLimite, Estado)

                VALUES
                (@BarcoId, @TipoMantenimiento, @Prioridad, @Descripcion, GETDATE(), @FechaLimite, 'Abierta')
            ", conn);

            cmd.Parameters.AddWithValue("@BarcoId", orden.BarcoId);
            cmd.Parameters.AddWithValue("@TipoMantenimiento", orden.TipoMantenimiento);
            cmd.Parameters.AddWithValue("@Prioridad", orden.Prioridad);
            cmd.Parameters.AddWithValue("@Descripcion", orden.Descripcion);
            cmd.Parameters.AddWithValue("@FechaLimite", orden.FechaLimite);

            cmd.ExecuteNonQuery();

            return Ok();
        }

        [HttpPost("Asignar")]
        public IActionResult Asignar(int ordenId, int personalId)
        {
            using var conn = new SqlConnection(
                _config.GetConnectionString("DefaultConnection"));

            conn.Open();

            var cmd = new SqlCommand(@"
                INSERT INTO OrdenServicioPersonal
                (OrdenId, PersonalId)

                VALUES
                (@OrdenId, @PersonalId)
            ", conn);

            cmd.Parameters.AddWithValue("@OrdenId", ordenId);
            cmd.Parameters.AddWithValue("@PersonalId", personalId);

            cmd.ExecuteNonQuery();

            return Ok();
        }

        [HttpPost("CambiarEstado")]
        public IActionResult CambiarEstado(int ordenId, string estado, string? informe)
        {
            using var conn = new SqlConnection(
                _config.GetConnectionString("DefaultConnection"));

            conn.Open();

            if (estado == "Cerrada")
            {
                var cmdCerrar = new SqlCommand(@"
                    UPDATE OrdenesServicio
                    SET Estado = @Estado,
                        InformeCierre = @Informe,
                        FechaCierreReal = GETDATE(),
                        UsuarioCierre = 'admin'
                    WHERE OrdenId = @OrdenId
                ", conn);

                cmdCerrar.Parameters.AddWithValue("@Estado", estado);
                cmdCerrar.Parameters.AddWithValue("@Informe", informe ?? "");
                cmdCerrar.Parameters.AddWithValue("@OrdenId", ordenId);

                cmdCerrar.ExecuteNonQuery();

                return Ok();
            }

            var cmd = new SqlCommand(@"
                UPDATE OrdenesServicio
                SET Estado = @Estado
                WHERE OrdenId = @OrdenId
            ", conn);

            cmd.Parameters.AddWithValue("@Estado", estado);
            cmd.Parameters.AddWithValue("@OrdenId", ordenId);

            cmd.ExecuteNonQuery();

            return Ok();
        }
    }
}