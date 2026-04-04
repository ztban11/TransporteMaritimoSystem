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
        private readonly IConfiguration _configuration;

        public AuthController(TransporteMaritimoContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var user = _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefault(u => u.sNombre == request.sNombre);

            if (user == null)
                return Unauthorized("Usuario no encontrado");

            if (user.sPasswordHash != request.sPassword)
                return Unauthorized("Password incorrecto");

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
            );

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.sNombre),
                new Claim(ClaimTypes.Email, user.sEmail),
                new Claim(ClaimTypes.Role, user.Rol?.sNombreRol ?? "Usuario")
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(
                    Convert.ToDouble(_configuration["Jwt:DurationInMinutes"])
                ),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { token = jwt });
        }
    }
}