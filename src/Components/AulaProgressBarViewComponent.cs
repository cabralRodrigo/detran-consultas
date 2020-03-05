using Microsoft.AspNetCore.Mvc;

namespace DetranConsulta.Components
{
    public class AulaProgressBarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int valor, int total, string label)
        {
            var porcentagem = (100 * valor) / total;

            if (porcentagem > 100)
            {
                porcentagem = 100;
            }

            if (porcentagem < 0)
            {
                porcentagem = 0;
            }

            var estilo = porcentagem switch
            {
                var x when x >= 0 && x <= 25 => "bg-danger",
                var x when x >= 26 && x <= 50 => "bg-warning",
                var x when x >= 51 && x <= 75 => "bg-info",
                var x when x >= 76 && x <= 99 => string.Empty,
                100 => "bg-success",
                _ => string.Empty
            };

            return this.View(new Model
            {
                Disciplina = label,
                Estilo = estilo,
                QuantidadeAulasFeitas = valor,
                QuantidadeAulasTotal = total,
                Porcentagem = porcentagem
            });
        }

        public class Model
        {
            public string Disciplina { get; set; }
            public string Estilo { get; set; }
            public int QuantidadeAulasFeitas { get; set; }
            public int QuantidadeAulasTotal { get; set; }
            public int Porcentagem { get; set; }
        }
    }
}