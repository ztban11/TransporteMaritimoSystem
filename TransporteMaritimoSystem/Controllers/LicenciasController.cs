using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using TransporteMaritimo.Core.Models;

namespace TransporteMaritimoSystem.Controllers
{
    public class LicenciasController : Controller
    {
        private readonly HttpClient _httpClient;

        public LicenciasController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5233/");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/licencias");

            if (!response.IsSuccessStatusCode)
                return View(new List<Licencia>());

            var json = await response.Content.ReadAsStringAsync();

            var licencias = JsonSerializer.Deserialize<List<Licencia>>(json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return View(licencias);
        }

        public IActionResult Create(int iElPersonalId)
        {
            var licencia = new Licencia { PersonalId= iElPersonalId,
            TipoLicencia = "",
            FechaExpiracion=DateTime.Now};
            return View(licencia);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Licencia licencia)
        {
            var json = JsonSerializer.Serialize(licencia);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await _httpClient.PostAsync("api/licencias", content);

            return RedirectToAction("Index","Personal");
        }
    }
}