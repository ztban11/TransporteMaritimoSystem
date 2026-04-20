using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TransporteMaritimo.Core.Models;   

namespace TransporteMaritimo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TravesiasController : ControllerBase
    {
        private readonly IConfiguration _config;
        public TravesiasController(IConfiguration config) { _config = config; }
        [HttpGet]
        public IActionResult Get()
        {
            var travesias = new List<Travesia>();
            using var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            conn.Open();
            var cmd = new SqlCommand(@"                SELECT *                FROM Travesias            ", conn);
            var reader = cmd.ExecuteReader();
            while (reader.Read()) { travesias.Add(new Travesia { TravesiaId = (int)reader["TravesiaId"], BarcoId = (int)reader["BarcoId"], PuertoOrigen = reader["PuertoOrigen"].ToString() ?? "", PuertoDestino = reader["PuertoDestino"].ToString() ?? "", FechaSalidaPrevista = (DateTime)reader["FechaSalidaPrevista"], FechaLlegadaPrevista = (DateTime)reader["FechaLlegadaPrevista"], Estado = reader["Estado"].ToString() ?? "", FechaCierreReal = reader["FechaCierreReal"] as DateTime?, UsuarioCierre = reader["UsuarioCierre"]?.ToString() }); }
            return Ok(travesias);
        }

        [HttpPost]
        public IActionResult Post(Travesia travesia)
        {

            if (travesia.PuertoOrigen == travesia.PuertoDestino)
            {
                return BadRequest("El puerto de origen y destino no pueden ser iguales.");
            }

            using var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            conn.Open();
            try
            {
                var cmd = new SqlCommand(@"                    INSERT INTO Travesias                    (                        BarcoId,                        PuertoOrigen,                        PuertoDestino,                        FechaSalidaPrevista,                        FechaLlegadaPrevista,                        Estado                    )                    VALUES                    (                        @BarcoId,                        @PuertoOrigen,                        @PuertoDestino,                        @FechaSalidaPrevista,                        @FechaLlegadaPrevista,                        'Planeada'                    )                ", conn);
                cmd.Parameters.AddWithValue("@BarcoId", travesia.BarcoId); cmd.Parameters.AddWithValue("@PuertoOrigen", travesia.PuertoOrigen); cmd.Parameters.AddWithValue("@PuertoDestino", travesia.PuertoDestino); cmd.Parameters.AddWithValue("@FechaSalidaPrevista", travesia.FechaSalidaPrevista); cmd.Parameters.AddWithValue("@FechaLlegadaPrevista", travesia.FechaLlegadaPrevista);
                cmd.ExecuteNonQuery();
                return Ok();
            }
            catch { return BadRequest("Error registrando travesía"); }
        }

        [HttpPost("AsignarTripulante")]
        public IActionResult AsignarTripulante(int travesiaId, int personalId)
        {
            using var conn = new SqlConnection(
                _config.GetConnectionString("DefaultConnection")
            );

            conn.Open();

            var cmd = new SqlCommand(@"
        INSERT INTO TravesiaPersonal (TravesiaId, PersonalId)
        VALUES (@TravesiaId, @PersonalId)
    ", conn);

            cmd.Parameters.AddWithValue("@TravesiaId", travesiaId);
            cmd.Parameters.AddWithValue("@PersonalId", personalId);

            cmd.ExecuteNonQuery();

            return Ok();
        }

        [HttpPost("CambiarEstado")]
        public IActionResult CambiarEstado(int travesiaId, string estado)
        {
            using var conn = new SqlConnection(
                _config.GetConnectionString("DefaultConnection")
            );

            conn.Open();

            if (estado == "En Curso")
            {
                var cmdValidacion = new SqlCommand(@"
            SELECT RolPrimario, COUNT(*) Cantidad
            FROM TravesiaPersonal TP
            JOIN Personal P ON TP.PersonalId = P.PersonalId
            WHERE TP.TravesiaId = @TravesiaId
            GROUP BY RolPrimario
        ", conn);

                cmdValidacion.Parameters.AddWithValue("@TravesiaId", travesiaId);

                var reader = cmdValidacion.ExecuteReader();

                int capitan = 0;
                int primerOficial = 0;
                int ingenieros = 0;
                int marineros = 0;

                while (reader.Read())
                {
                    var rol = reader["RolPrimario"].ToString();
                    var cantidad = (int)reader["Cantidad"];

                    if (rol == "Capitan") capitan = cantidad;
                    if (rol == "PrimerOficial") primerOficial = cantidad;
                    if (rol == "Ingeniero") ingenieros = cantidad;
                    if (rol == "Marinero") marineros = cantidad;
                }

                reader.Close();

                if (capitan < 1 || primerOficial < 1 || ingenieros < 2 || marineros < 5)
                {
                    return BadRequest("La travesía no cumple con la tripulación mínima requerida.");
                }
            }

            // Cuando estado pase a 'Completada'
            SqlCommand cmdUpdate;

            if (estado == "Completada")
            {
                cmdUpdate = new SqlCommand(@"
            UPDATE Travesias
            SET Estado = @Estado,
                FechaCierreReal = GETDATE(),
                UsuarioCierre = @Usuario
            WHERE TravesiaId = @TravesiaId
        ", conn);

                cmdUpdate.Parameters.AddWithValue("@Usuario", "admin"); // luego podemos usar usuario logueado
            }
            else
            {
                cmdUpdate = new SqlCommand(@"
            UPDATE Travesias
            SET Estado = @Estado
            WHERE TravesiaId = @TravesiaId
        ", conn);
            }

            cmdUpdate.Parameters.AddWithValue("@Estado", estado);
            cmdUpdate.Parameters.AddWithValue("@TravesiaId", travesiaId);

            cmdUpdate.ExecuteNonQuery();

            return Ok();
        }

    }
}
