using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransporteMaritimo.Data.Context;

namespace TransporteMaritimo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistorialRolesController : ControllerBase
    {
        private readonly TransporteMaritimoContext _context;

        public HistorialRolesController(TransporteMaritimoContext context)
        {
            _context = context;
        }

        // GET api/historialroles
        [HttpGet]
        public async Task<IActionResult> GetHistorial()
        {
            var historial = await _context.HistorialCambiosRol
                .OrderByDescending(h => h.FechaCambio)
                .Select(h => new
                {
                    Usuario = _context.Usuarios
                        .Where(u => u.iUsuarioId == h.UsuarioId)
                        .Select(u => u.sNombre)
                        .FirstOrDefault(),

                    RolAnterior = _context.Roles
                        .Where(r => r.iRolId == h.RolAnteriorId)
                        .Select(r => r.sNombreRol)
                        .FirstOrDefault(),

                    RolNuevo = _context.Roles
                        .Where(r => r.iRolId == h.RolNuevoId)
                        .Select(r => r.sNombreRol)
                        .FirstOrDefault(),

                    Administrador = _context.Usuarios
                        .Where(u => u.iUsuarioId == h.ModificadoPorUsuarioId)
                        .Select(u => u.sNombre)
                        .FirstOrDefault(),

                    FechaCambio = h.FechaCambio
                })
                .ToListAsync();

            return Ok(historial);
        }
    }
}