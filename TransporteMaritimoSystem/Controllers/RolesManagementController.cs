using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace TransporteMaritimoSystem.Controllers
{
    public class RolesManagementController : Controller
    {
        private readonly HttpClient _httpClient;
        public RolesManagementController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
        }
        public async Task<IActionResult> Index()
        {
            var usuarios = await _httpClient.GetFromJsonAsync<List<object>>(
                "https://localhost:5001/api/usuarios");

            return View(usuarios);
        }
    }
}
