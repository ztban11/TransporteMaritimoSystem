using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransporteMaritimo.Core.Models;
using TransporteMaritimo.Data.Context;

namespace TransporteMaritimo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly TransporteMaritimoContext _context;

        public UsuariosController(TransporteMaritimoContext context)
        {
            _context = context;
        }

        // GET: api/usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var usuarios = await _context.Usuarios
                .Include(u => u.UsuarioRoles)
                .ThenInclude(ur => ur.Rol)
                .ToListAsync();

            return Ok(usuarios);
        }

        // GET api/usuarios/{userId}/roles
        [HttpGet("{userId}/roles")]
        public IActionResult GetUserRoles(int userId)
        {
            var user = _context.Usuarios
                .Include(u => u.UsuarioRoles)
                .ThenInclude(ur => ur.Rol)
                .FirstOrDefault(u => u.iUsuarioId == userId);

            if (user == null)
                return NotFound();

            var roles = user.UsuarioRoles
                .Where(ur => ur.Rol != null)
                .Select(ur => new
                {
                    RolId = ur.Rol!.iRolId,
                    NombreRol = ur.Rol!.sNombreRol
                })
                .ToList();

            return Ok(roles);
        }
    }
}