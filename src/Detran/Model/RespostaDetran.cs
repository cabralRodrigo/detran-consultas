namespace DetranConsulta.Detran.Model
{
    public class RespostaDetran<T>
    {
        public long TempoTotal { get; set; }
        public long TempoDetran { get; set; }
        public T Valor { get; set; }
    }
}