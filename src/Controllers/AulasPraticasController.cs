using DetranConsulta.Detran;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DetranConsulta.Controllers
{
    public class AulasPraticasController : Controller
    {
        private readonly IRenachStorage renachStorage;
        private readonly IDetranApi detranApi;

        public AulasPraticasController(IRenachStorage renachStorage, IDetranApi detranApi)
        {
            this.renachStorage = renachStorage;
            this.detranApi = detranApi;
        }

        public IActionResult Index() => this.View();

        public async Task<IActionResult> Consultar(string renach)
        {
            var (aulas, tempoTotal, tempoDetran) = await this.detranApi.ListarAulasPraticas(renach);
            this.renachStorage.DefinirRenach(renach);

            return this.View((aulas, tempoTotal, tempoDetran));
        }
    }
}