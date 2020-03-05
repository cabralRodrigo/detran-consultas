using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DetranConsulta.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDetranApi detranApi;

        public HomeController(IDetranApi detranApi)
        {
            this.detranApi = detranApi;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> Aulas(string renach)
        {
            var (aulas, tempoTotal, tempoDetran) = await this.detranApi.ListarAulas(renach);
            this.Response.Cookies.Append("renach", renach, new CookieOptions
            {
                Expires = new DateTimeOffset(2038, 1, 1, 0, 0, 0, TimeSpan.FromHours(0))
            });

            return this.View((aulas, tempoTotal, tempoDetran));
        }
    }
}