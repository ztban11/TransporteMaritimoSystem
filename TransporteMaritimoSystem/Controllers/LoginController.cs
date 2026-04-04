using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using TransporteMaritimoSystem.Models;

namespace TransporteMaritimoSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;

        public LoginController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5233/");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var json = JsonSerializer.Serialize(model);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Auth/login", content);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Usuario o contraseña incorrectos";
                return View();
            }

            var result = await response.Content.ReadAsStringAsync();

            var token = JsonDocument.Parse(result)
                        .RootElement
                        .GetProperty("token")
                        .GetString();

            if (string.IsNullOrEmpty(token))
            {
                ViewBag.Error = "Error obteniendo token";
                return View();
            }
            HttpContext.Session.SetString("JWToken", token);

            return RedirectToAction("Index", "Dashboard");
        }
    }
}