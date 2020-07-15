using System.Collections.Generic;
using System.Threading.Tasks;

namespace DetranConsulta.Detran
{
    public interface IDetranApi
    {
        Task<(List<Aula> Aulas, long TempoTotal, long TempoDetran)> ListarAulas(string renach);
        Task<(List<AulaPratica> Aulas, long TempoTotal, long TempoDetran)> ListarAulasPraticas(string renach);
    }
}