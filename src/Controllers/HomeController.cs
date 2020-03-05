using Microsoft.AspNetCore.Mvc;

namespace DetranConsulta.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => this.View();
    }
}