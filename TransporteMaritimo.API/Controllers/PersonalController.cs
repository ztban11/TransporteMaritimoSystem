using Microsoft.AspNetCore.Mvc;
using TransporteMaritimo.Data.Context;
using TransporteMaritimo.Core.Models;

namespace TransporteMaritimo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonalController : ControllerBase
    {
        private readonly TransporteMaritimoContext _context;

        public PersonalController(TransporteMaritimoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetPersonal()
        {
            var personal = _context.Personal.ToList();

            return Ok(personal);
        }

        [HttpPost]
        public IActionResult Create(Personal model)
        {
            // Validar Identificación única
            var exists = _context.Personal
                .Any(p => p.Identificacion == model.Identificacion);

            if (exists)
                return BadRequest("Ya existe un personal con esa identificación");

            _context.Personal.Add(model);

            _context.SaveChanges();

            return Ok();
        }
    }
}