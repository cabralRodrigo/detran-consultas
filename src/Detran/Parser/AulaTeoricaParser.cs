using DetranConsulta.Detran.Model;

namespace DetranConsulta.Detran.Parser
{
    internal class AulaTeoricaParser : AulaParser<AulaTeorica>
    {
        protected override void ParseAula(AulaTeorica aula, string[] dados)
        {
            aula.Disciplina = dados[3] switch
            {
                "DIREÇÃO DEFENSIVA" => DisciplinaTeorica.DirecaoDefensiva,
                "PRIMEIROS SOCORROS" => DisciplinaTeorica.PrimeirosSocorros,
                "LEGISLAÇÃO DE TRÂNSITO" => DisciplinaTeorica.Legislacao,
                "NOÇÕES DE MECÂNICA VEICULAR" => DisciplinaTeorica.Mecanica,
                "MEIO AMBIENTE CIDADANIA" => DisciplinaTeorica.MeioAmbiente,
                _ => DisciplinaTeorica.Desconhecida
            };
        }
    }
}