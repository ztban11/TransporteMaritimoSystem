using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TransporteMaritimo.API.Models;
using TransporteMaritimo.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace TransporteMaritimo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly TransporteMaritimoContext _context;

        public AuthController(TransporteMaritimoContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var user = _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefault(u => u.sEmail == request.sEmail);

            if (user == null)
                return Unauthorized("Usuario no encontrado");

            if (user.sPasswordHash != request.sPassword)
                return Unauthorized("Password incorrecto");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SUPER_SECRET_KEY_12345"));

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.sNombre),
                new Claim(ClaimTypes.Email, user.sEmail),
                new Claim(ClaimTypes.Role, user.Rol?.sNombreRol ?? "Usuario")
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { token = jwt });
        }
    }
}