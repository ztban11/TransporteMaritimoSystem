using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using TransporteMaritimo.Core.Models;
using TransporteMaritimoSystem.Models;

namespace TransporteMaritimoSystem.Controllers
{
    public class RolesController : Controller
    {
        private readonly HttpClient _httpClient;

        public RolesController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5233/");
        }

        // Lista de Usuarios
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/usuarios");

            if (!response.IsSuccessStatusCode)
                return View(new List<UsuarioDTO>());

            var json = await response.Content.ReadAsStringAsync();

            var usuarios = JsonSerializer.Deserialize<List<UsuarioDTO>>(json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<UsuarioDTO>();

            return View(usuarios);
        }

        // Editar Roles
        public async Task<IActionResult> Edit(int id)
        {
            // Obtener usuarios
            var usersResponse = await _httpClient.GetAsync("api/usuarios");

            var usersJson = await usersResponse.Content.ReadAsStringAsync();

            var users = JsonSerializer.Deserialize<List<UsuarioDTO>>(usersJson,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ?? new List<UsuarioDTO>();

            var user = users.FirstOrDefault(u => u.iUsuarioId == id);

            if (user == null)
                return RedirectToAction("Index");

            // Obtener roles del sistema
            var rolesResponse = await _httpClient.GetAsync("api/roles");

            var rolesJson = await rolesResponse.Content.ReadAsStringAsync();

            var roles = JsonSerializer.Deserialize<List<Rol>>(rolesJson,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ?? new List<Rol>();

            // Obtener roles actuales del usuario
            var userRolesResponse = await _httpClient.GetAsync($"api/usuarios/{id}/roles");

            var userRolesJson = await userRolesResponse.Content.ReadAsStringAsync();

            var userRoles = JsonSerializer.Deserialize<List<UsuarioRolDTO>>(userRolesJson,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ?? new List<UsuarioRolDTO>();

            var model = new EditUserRolesViewModel
            {
                iUsuarioId = user.iUsuarioId,
                sNombre = user.sNombre,

                Roles = roles.Select(r => new RoleCheckbox
                {
                    iRolId = r.iRolId,
                    sNombreRol = r.sNombreRol,
                    bSeleccionada = userRoles.Any(ur => ur.rolId == r.iRolId)
                }).ToList()
            };

            return View(model);
        }

        // GUARDAR CAMBIOS
        [HttpPost]
        public async Task<IActionResult> EditRoles(EditUserRolesViewModel model)
        {
            var selectedRoles = model.Roles
                .Where(r => r.bSeleccionada)
                .Select(r => r.iRolId)
                .ToList();

            var json = JsonSerializer.Serialize(selectedRoles);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await _httpClient.PostAsync(
                $"api/usuarioroles/{model.iUsuarioId}/roles",
                content);

            return RedirectToAction("Index");
        }
    }
}