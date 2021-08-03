using DetranConsulta.Detran.Model;
using DetranConsulta.Detran.Parser;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace DetranConsulta.Detran
{
    public class DetranApi : IDetranApi
    {
        private readonly HttpClient http;

        public DetranApi(HttpClient http)
        {
            this.http = http;
        }

        public Task<RespostaDetran<List<AulaPratica>>> ListarAulasPraticas(string renach)
        {
            return this.Aulas("pratica", renach, new AulaPraticaParser());
        }

        public Task<RespostaDetran<List<AulaTeorica>>> ListarAulasTeoricas(string renach)
        {
            return this.Aulas("detalhesTeorico", renach, new AulaTeoricaParser());
        }

        private async Task<RespostaDetran<T>> Aulas<T>(string tipo, string renach, IParser<T> parser)
        {
            var tempoTotal = Stopwatch.StartNew();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://www2.detran.rj.gov.br/portal/habilitacao/biometriaValid")
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    ["renach"] = renach,
                    ["disciplina"] = "TEORICAS",
                    ["tipo"] = tipo
                })
            };

            request.Headers.Add("X-Requested-With", "XMLHttpRequest");

            var tempoDetran = Stopwatch.StartNew();
            var response = await this.http.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            tempoDetran.Stop();

            var html = new HtmlDocument();
            html.LoadHtml(content);

            var resultado = parser.Parse(html);

            tempoTotal.Stop();

            return new RespostaDetran<T>
            {
                TempoDetran = tempoDetran.ElapsedMilliseconds,
                TempoTotal = tempoTotal.ElapsedMilliseconds,
                Valor = resultado
            };
        }
    }
}