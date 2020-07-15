using DetranConsulta.Detran.Model;

namespace DetranConsulta.Detran.Parser
{
    public class AulaPraticaParser : AulaParser<AulaPratica>
    {
        protected override void ParseAula(AulaPratica aula, string[] dados)
        {
            aula.Disciplina = dados[3] switch
            {
                "PRATICO DE DIRECAO VEICULAR - AUTO" => DisciplinaPratica.Carro,
                _ => DisciplinaPratica.Desconhecida
            };
        }
    }
}