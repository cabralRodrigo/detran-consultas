using System.ComponentModel;

namespace DetranConsulta.Detran
{
    public enum Disciplina
    {
        [Description("Direção Defensiva")]
        [QuantidadeAulas(16)]
        DirecaoDefensiva,

        [Description("Primeiros Socorros")]
        [QuantidadeAulas(4)]
        PrimeirosSocorros,

        [Description("Legislação de trânsito")]
        [QuantidadeAulas(18)]
        Legislacao,

        [Description("Mecânica")]
        [QuantidadeAulas(3)]
        Mecanica,

        [Description("Meio Ambiente")]
        [QuantidadeAulas(4)]
        MeioAmbiente
    }
}