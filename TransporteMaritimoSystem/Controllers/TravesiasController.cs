using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TransporteMaritimo.Core.Models;

namespace TransporteMaritimoSystem.Controllers
{
    public class TravesiasController : Controller
    {
        private readonly HttpClient _httpClient;

        public TravesiasController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5233/");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/travesias");

            if (!response.IsSuccessStatusCode)
                return View(new List<Travesia>());

            var json = await response.Content.ReadAsStringAsync();

            var travesias = JsonSerializer.Deserialize<List<Travesia>>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            ) ?? new List<Travesia>();

            return View(travesias);
        }

        // GET CREATE
        public async Task<IActionResult> Create()
        {
            var response = await _httpClient.GetAsync("api/barcos");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Barcos = new List<Barco>();
                return View();
            }

            var json = await response.Content.ReadAsStringAsync();

            var barcos = JsonSerializer.Deserialize<List<Barco>>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            ) ?? new List<Barco>();

            ViewBag.Barcos = barcos;

            return View();
        }

        // POST CREATE
        [HttpPost]
        public async Task<IActionResult> Create(Travesia model)
        {
            var json = JsonSerializer.Serialize(model);

            var content = new StringContent(
                json,
                System.Text.Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync("api/travesias", content);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Error registrando travesía";
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CambiarEstado([FromBody] TravesiaEstadoDto dto)
        {
            var json = JsonSerializer.Serialize(dto);

            var content = new StringContent(
                json,
                System.Text.Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync("api/travesias/CambiarEstado", content);

            if (!response.IsSuccessStatusCode)
                return BadRequest();

            return Ok();
        }
    }
}
