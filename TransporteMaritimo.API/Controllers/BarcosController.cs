using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TransporteMaritimo.Core.Models;

namespace TransporteMaritimo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BarcosController : ControllerBase
    {
        private readonly IConfiguration _config;

        public BarcosController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var barcos = new List<Barco>();

            using var conn = new SqlConnection(
                _config.GetConnectionString("DefaultConnection")
            );

            conn.Open();

            var cmd = new SqlCommand(@"
                SELECT 
                    b.*,
                    m.Modelo,
                    m.PotenciaHP,
                    m.HorasUso
                FROM Barcos b
                LEFT JOIN Motores m
                ON b.BarcoId = m.BarcoId
            ", conn);

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                barcos.Add(new Barco
                {
                    BarcoId = (int)reader["BarcoId"],
                    NombreBarco = reader["NombreBarco"].ToString() ?? "",
                    Tipo = reader["Tipo"].ToString() ?? "",
                    Matricula = reader["Matricula"].ToString() ?? "",
                    CapacidadCarga = (decimal)reader["CapacidadCarga"],
                    PuertoBase = reader["PuertoBase"].ToString() ?? "",
                    Activo = (bool)reader["Activo"],

                    ModeloMotor = reader["Modelo"]?.ToString() ?? "",
                    PotenciaHP = reader["PotenciaHP"] as int? ?? 0,
                    HorasUso = reader["HorasUso"] as int? ?? 0
                });
            }

            return Ok(barcos);
        }

        [HttpPost]
        public IActionResult Post(Barco barco)
        {
            using var conn = new SqlConnection(
                _config.GetConnectionString("DefaultConnection")
            );

            conn.Open();


            // Validar matrícula duplicada
            var cmdCheck = new SqlCommand(
                "SELECT COUNT(*) FROM Barcos WHERE Matricula = @Matricula",
                conn
            );

            cmdCheck.Parameters.AddWithValue("@Matricula", barco.Matricula);

            var existe = (int)cmdCheck.ExecuteScalar();

            if (existe > 0)
            {
                return BadRequest("La matrícula ya está registrada para otro barco");
            }


            var tran = conn.BeginTransaction();

            try
            {
                var cmdBarco = new SqlCommand(@"
                    INSERT INTO Barcos
                    (NombreBarco, Tipo, Matricula, CapacidadCarga, PuertoBase, Activo)

                    VALUES
                    (@NombreBarco, @Tipo, @Matricula, @CapacidadCarga, @PuertoBase, 1);

                    SELECT SCOPE_IDENTITY();
                ", conn, tran);

                cmdBarco.Parameters.AddWithValue("@NombreBarco", barco.NombreBarco);
                cmdBarco.Parameters.AddWithValue("@Tipo", barco.Tipo);
                cmdBarco.Parameters.AddWithValue("@Matricula", barco.Matricula);
                cmdBarco.Parameters.AddWithValue("@CapacidadCarga", barco.CapacidadCarga);
                cmdBarco.Parameters.AddWithValue("@PuertoBase", barco.PuertoBase);

                var barcoId = Convert.ToInt32(cmdBarco.ExecuteScalar());

                var cmdMotor = new SqlCommand(@"
                    INSERT INTO Motores
                    (BarcoId, Modelo, PotenciaHP, HorasUso)

                    VALUES
                    (@BarcoId, @Modelo, @PotenciaHP, @HorasUso)
                ", conn, tran);

                cmdMotor.Parameters.AddWithValue("@BarcoId", barcoId);
                cmdMotor.Parameters.AddWithValue("@Modelo", barco.ModeloMotor);
                cmdMotor.Parameters.AddWithValue("@PotenciaHP", barco.PotenciaHP);
                cmdMotor.Parameters.AddWithValue("@HorasUso", barco.HorasUso);

                cmdMotor.ExecuteNonQuery();

                tran.Commit();

                return Ok();
            }
            catch
            {
                tran.Rollback();
                return BadRequest("Error registrando barco");
            }
        }
    }
}
