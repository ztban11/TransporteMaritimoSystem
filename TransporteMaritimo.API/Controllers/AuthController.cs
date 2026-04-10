using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TransporteMaritimo.API.Models;
using TransporteMaritimo.Data.Context;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

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
            var maxAttempts = _configuration.GetValue<int>("Security:MaxLoginAttempts");
            var lockoutMinutes = _configuration.GetValue<int>("Security:LockoutMinutes");

            var user = _context.Usuarios
    .Include(u => u.UsuarioRoles)
        .ThenInclude(ur => ur.Rol)
    .FirstOrDefault(u => u.sNombre == request.sNombre);

            if (user == null)
                return Unauthorized("Usuario no encontrado");

            if (user.dtBloqueadoHasta != null && user.dtBloqueadoHasta > DateTime.Now)
                return Unauthorized("Usuario bloqueado temporalmente");

            if (!BCrypt.Net.BCrypt.Verify(request.sPassword, user.sPasswordHash))
            {
                user.iIntentosFallidos++;

                if (user.iIntentosFallidos >= maxAttempts)
                {
                    user.dtBloqueadoHasta = DateTime.Now.AddMinutes(lockoutMinutes);
                    user.iIntentosFallidos = 0;
                }

                _context.SaveChanges();

                return Unauthorized("Password incorrecto");
            }

            user.iIntentosFallidos = 0;
            user.dtBloqueadoHasta = null;

            _context.SaveChanges();

            var jwtKey = _configuration["Jwt:Key"]
                ?? throw new Exception("JWT Key not configured");

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey)
            );

            var roles = user.UsuarioRoles
                .Where(ur => ur.Rol != null)
                .Select(ur => ur.Rol!.sNombreRol)
                .ToList();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.sNombre),
                new Claim(ClaimTypes.NameIdentifier, user.iUsuarioId.ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

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