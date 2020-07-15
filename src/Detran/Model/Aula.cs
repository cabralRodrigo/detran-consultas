using System;
using System.ComponentModel.DataAnnotations;

namespace DetranConsulta.Detran.Model
{
    public abstract class Aula
    {
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Data { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime DataEnvio { get; set; }

        [DisplayFormat(DataFormatString = "{0:HH:mm}")]
        public TimeSpan Inicio { get; set; }

        [DisplayFormat(DataFormatString = "{0:HH:mm}")]
        public TimeSpan Fim { get; set; }

        public string Status { get; set; }
    }
}