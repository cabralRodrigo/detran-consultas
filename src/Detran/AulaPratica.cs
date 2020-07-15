using System;

namespace DetranConsulta.Detran
{
    public class AulaPratica
    {
        public DateTime Data { get; set; }
        public DateTime DataEnvio { get; set; }
        public TimeSpan Inicio { get; set; }
        public TimeSpan Fim { get; set; }
        public string Disciplina { get; set; }
        public string Status { get; set; }
    }
}