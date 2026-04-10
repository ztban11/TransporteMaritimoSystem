using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TransporteMaritimoSystem.Models;

namespace TransporteMaritimoSystem.Controllers
{
    public class HistorialRolesController : Controller
    {
        private readonly HttpClient _httpClient;

        public HistorialRolesController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5233/");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/historialroles");

            if (!response.IsSuccessStatusCode)
                return View(new List<HistorialRolViewModel>());

            var json = await response.Content.ReadAsStringAsync();

            var historial = JsonSerializer.Deserialize<List<HistorialRolViewModel>>(json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<HistorialRolViewModel>();

            return View(historial);
        }
    }
}