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

        [HttpPost("{adminId}/{userId}/roles")]
        public IActionResult AssignRoles(int adminId, int userId, [FromBody] List<int> roles)
        {
            Console.WriteLine("DEBUG: Entró al método AssignRoles");
            var user = _context.Usuarios.Find(userId);

            if (user == null)
                return NotFound("Usuario no existe");

            // Obtener roles actuales
            var currentRoles = _context.UsuariosRoles
                .Where(x => x.UsuarioId == userId)
                .Select(x => x.RolId)
                .ToList();

            // Roles nuevos válidos
            var validRoles = _context.Roles
                .Where(r => roles.Contains(r.iRolId))
                .Select(r => r.iRolId)
                .ToList();

            // Roles agregados
            var rolesAdded = validRoles.Except(currentRoles).ToList();

            // Roles removidos
            var rolesRemoved = currentRoles.Except(validRoles).ToList();

            Console.WriteLine("Roles actuales: " + string.Join(",", currentRoles));
            Console.WriteLine("Roles enviados: " + string.Join(",", roles));
            Console.WriteLine("Roles válidos: " + string.Join(",", validRoles));
            Console.WriteLine("Roles agregados: " + string.Join(",", rolesAdded));
            Console.WriteLine("Roles removidos: " + string.Join(",", rolesRemoved));

            // Registrar roles agregados
            foreach (var roleId in rolesAdded)
            {
                _context.HistorialCambiosRol.Add(new HistorialCambiosRol
                {
                    UsuarioId = userId,
                    RolAnteriorId = null,
                    RolNuevoId = roleId,
                    ModificadoPorUsuarioId = adminId,
                    FechaCambio = DateTime.Now
                });
            }

            // Registrar roles eliminados
            foreach (var roleId in rolesRemoved)
            {
                _context.HistorialCambiosRol.Add(new HistorialCambiosRol
                {
                    UsuarioId = userId,
                    RolAnteriorId = roleId,
                    RolNuevoId = null,
                    ModificadoPorUsuarioId = adminId,
                    FechaCambio = DateTime.Now
                });
            }

            // Eliminar roles actuales
            var existing = _context.UsuariosRoles
                .Where(x => x.UsuarioId == userId);

            _context.UsuariosRoles.RemoveRange(existing);

            // Insertar nuevos roles
            foreach (var roleId in validRoles)
            {
                _context.UsuariosRoles.Add(new UsuarioRol
                {
                    UsuarioId = userId,
                    RolId = roleId
                });
            }

            Console.WriteLine("Historial entries: " + _context.ChangeTracker.Entries().Count());

            _context.SaveChanges();

            return Ok("Roles actualizados correctamente");
        }
    }
}