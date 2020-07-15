using DetranConsulta.Detran;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DetranConsulta.Controllers
{
    public class AulasTeoricasController : Controller
    {
        private readonly IRenachStorage renachStorage;
        private readonly IDetranApi detranApi;

        public AulasTeoricasController(IRenachStorage renachStorage, IDetranApi detranApi)
        {
            this.renachStorage = renachStorage;
            this.detranApi = detranApi;
        }

        public IActionResult Index() => this.View();

        public async Task<IActionResult> Consultar(string renach)
        {
            var aulas = await this.detranApi.ListarAulasTeoricas(renach);
            this.renachStorage.DefinirRenach(renach);

            return this.View(aulas);
        }
    }
}