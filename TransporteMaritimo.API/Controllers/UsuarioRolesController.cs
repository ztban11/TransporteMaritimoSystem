using Microsoft.AspNetCore.Mvc;
using TransporteMaritimo.Core.Models;
using TransporteMaritimo.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace TransporteMaritimoSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioRolesController : ControllerBase
    {
        private readonly TransporteMaritimoContext _context;

        public UsuarioRolesController(TransporteMaritimoContext context)
        {
            _context = context;
        }

        [HttpPost("{userId}/roles")]
        public IActionResult AssignRoles(int userId, [FromBody] List<int> roles)
        {
            var user = _context.Usuarios.Find(userId);

            if (user == null)
                return NotFound("Usuario no existe");

            // Validate roles exist
            var validRoles = _context.Roles
                .Where(r => roles.Contains(r.iRolId))
                .Select(r => r.iRolId)
                .ToList();

            if (!validRoles.Any())
                return BadRequest("Roles inválidos");

            // Remove previous roles
            var existing = _context.UsuariosRoles
                .Where(x => x.UsuarioId == userId);

            _context.UsuariosRoles.RemoveRange(existing);

            foreach (var roleId in validRoles)
            {
                _context.UsuariosRoles.Add(new UsuarioRol
                {
                    UsuarioId = userId,
                    RolId = roleId
                });
            }

            _context.SaveChanges();

            return Ok("Roles asignados correctamente");
        }
    }
}
