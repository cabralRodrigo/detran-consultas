namespace DetranConsulta.Detran.Model
{
    public enum DisciplinaPratica
    {
        [QuantidadeAulas(20)]
        Carro = 1,

        [QuantidadeAulas(20)]
        Moto = 2,

        Desconhecida = 3
    }
}