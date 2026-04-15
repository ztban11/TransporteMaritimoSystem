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
            // Obtener Personal
            var responsePersonal = await _httpClient.GetAsync("api/personal");

            if (!responsePersonal.IsSuccessStatusCode)
                return View(new List<Personal>());

            var jsonPersonal = await responsePersonal.Content.ReadAsStringAsync();

            var personal = JsonSerializer.Deserialize<List<Personal>>(jsonPersonal,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<Personal>();


            // Obtener Licencias
            var responseLicencias = await _httpClient.GetAsync("api/licencias");

            List<Licencia> licencias = new();

            if (responseLicencias.IsSuccessStatusCode)
            {
                var jsonLicencias = await responseLicencias.Content.ReadAsStringAsync();

                licencias = JsonSerializer.Deserialize<List<Licencia>>(jsonLicencias,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? new List<Licencia>();
            }

            ViewBag.Licencias = licencias;

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

            var content = new StringContent(json,
                System.Text.Encoding.UTF8,
                "application/json");

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