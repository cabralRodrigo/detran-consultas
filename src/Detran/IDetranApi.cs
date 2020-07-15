using DetranConsulta.Detran.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DetranConsulta.Detran
{
    public interface IDetranApi
    {
        Task<RespostaDetran<List<AulaTeorica>>> ListarAulasTeoricas(string renach);
        Task<RespostaDetran<List<AulaPratica>>> ListarAulasPraticas(string renach);
    }
}