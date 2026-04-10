using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TransporteMaritimo.Core.Models;

namespace TransporteMaritimoSystem.Controllers
{
    public class PersonalController : Controller
    {
        private readonly HttpClient _httpClient;

        public PersonalController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5233/");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/personal");

            if (!response.IsSuccessStatusCode)
                return View(new List<Personal>());

            var json = await response.Content.ReadAsStringAsync();

            var personal = JsonSerializer.Deserialize<List<Personal>>(json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<Personal>();

            return View(personal);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Personal model)
        {
            var json = JsonSerializer.Serialize(model);

            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/personal", content);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Error registrando el personal";
                return View(model);
            }

            return RedirectToAction("Index");
        }
    }
}