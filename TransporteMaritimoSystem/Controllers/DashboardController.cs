using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace TransporteMaritimoSystem.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
