using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TransporteMaritimo.Core.Models;

namespace TransporteMaritimo.Web.Controllers
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

            var json = await response.Content.ReadAsStringAsync();

            var personal = JsonSerializer.Deserialize<List<Personal>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

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

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await _httpClient.PostAsync("api/personal", content);

            return RedirectToAction("Index");
        }
    }
}