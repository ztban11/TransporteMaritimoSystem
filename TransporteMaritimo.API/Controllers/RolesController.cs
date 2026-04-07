using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransporteMaritimo.Data.Context;
using TransporteMaritimo.Core.Models;


namespace TransporteMaritimo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly TransporteMaritimoContext _context;

        public RolesController(TransporteMaritimoContext context)
        {
            _context = context;
        }

        // GET api/roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rol>>> GetRoles()
        {
            var roles = await _context.Roles.ToListAsync();
            return Ok(roles);
        }
    }
}