using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace TransporteMaritimoSystem.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
