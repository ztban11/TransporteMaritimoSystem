
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TransporteMaritimo.Core.Models;

namespace TransporteMaritimoSystem.Controllers
{
    public class BarcosController : Controller
    {
        private readonly HttpClient _httpClient;

        public BarcosController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5233/");
        }
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/barcos");

            if (!response.IsSuccessStatusCode)
                return View(new List<Barco>());

            var json = await response.Content.ReadAsStringAsync();

            var barcos = JsonSerializer.Deserialize<List<Barco>>(json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<Barco>();

            return View(barcos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Barco model)
        {
            var json = JsonSerializer.Serialize(model);

            var content = new StringContent(
                json,
                System.Text.Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync("api/barcos", content);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Error registrando el barco: matrícula duplicada.";
                return View(model);
            }

            return RedirectToAction("Index");
        }
    }
}
