using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TransporteMaritimo.Core.Models;

namespace TransporteMaritimoSystem.Controllers
{
    public class OrdenesServicioController : Controller
    {
        private readonly HttpClient _http;

        public OrdenesServicioController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient();
            _http.BaseAddress = new Uri("http://localhost:5233/");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _http.GetAsync("api/ordenesservicio");

            if (!response.IsSuccessStatusCode)
                return View(new List<OrdenServicio>());

            var json = await response.Content.ReadAsStringAsync();

            var ordenes = JsonSerializer.Deserialize<List<OrdenServicio>>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return View(ordenes ?? new List<OrdenServicio>());
        }

        public async Task<IActionResult> Create()
        {
            var response = await _http.GetAsync("api/barcos");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Barcos = new List<Barco>();
                return View();
            }

            var json = await response.Content.ReadAsStringAsync();

            var barcos = JsonSerializer.Deserialize<List<Barco>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            ViewBag.Barcos = barcos ?? new List<Barco>();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrdenServicio model)
        {
            var json = JsonSerializer.Serialize(model);

            var content = new StringContent(
                json,
                System.Text.Encoding.UTF8,
                "application/json"
            );

            var response = await _http.PostAsync("api/ordenesservicio", content);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Error creando orden";

                var barcosResponse = await _http.GetAsync("api/barcos");
                var barcosJson = await barcosResponse.Content.ReadAsStringAsync();

                ViewBag.Barcos = JsonSerializer.Deserialize<List<Barco>>(barcosJson,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return View(model);
            }

            return RedirectToAction("Index");
        }
    }
}