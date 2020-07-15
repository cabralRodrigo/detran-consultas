using System.ComponentModel;

namespace DetranConsulta.Detran.Model
{
    public enum DisciplinaTeorica
    {
        [QuantidadeAulas(16)]
        [Description("Direção Defensiva")]
        DirecaoDefensiva = 1,

        [QuantidadeAulas(4)]
        [Description("Primeiros Socorros")]
        PrimeirosSocorros = 2,

        [QuantidadeAulas(18)]
        [Description("Legislação de trânsito")]
        Legislacao = 3,

        [QuantidadeAulas(3)]
        [Description("Mecânica")]
        Mecanica = 4,

        [QuantidadeAulas(4)]
        [Description("Meio Ambiente")]
        MeioAmbiente = 5,

        Desconhecida = 6
    }
}