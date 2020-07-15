using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        public async Task<(List<Aula> Aulas, long TempoTotal, long TempoDetran)> ListarAulas(string renach)
        {
            var tempoTotal = Stopwatch.StartNew();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://www2.detran.rj.gov.br/portal/habilitacao/biometriaValid")
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    ["renach"] = renach,
                    ["disciplina"] = "TEORICAS",
                    ["tipo"] = "detalhesTeorico"
                })
            };

            var tempoDetran = Stopwatch.StartNew();
            var html = await (await this.http.SendAsync(request)).Content.ReadAsStringAsync();
            tempoDetran.Stop();

            var document = new HtmlDocument();
            document.LoadHtml(html);

            var aulas = new List<Aula>();

            foreach (var linha in document.QuerySelectorAll("tr").Skip(1))
            {
                var dados = linha.ChildNodes.Where(s => s.Name == "td").Select(s => s.InnerText).ToArray();

                var sdata = dados[0];
                var sinicio = dados[1];
                var sfim = dados[2];
                var sdisciplina = dados[3];
                var senvio = dados[4];
                var sstatus = dados[5];

                var data = DetranDate(sdata);
                var inicio = DetranTime(sinicio, false);
                var fim = DetranTime(sfim, false);
                var envio = DetranDate(senvio);
                var disciplina = DetranDis(sdisciplina);

                aulas.Add(new Aula
                {
                    Data = new DateTime(data.Year, data.Month, data.Day, 0, 0, 0),
                    DataEnvio = envio,
                    Inicio = inicio,
                    Fim = fim,
                    Disciplina = disciplina,
                    Status = sstatus
                });
            }

            tempoTotal.Stop();

            return (aulas, tempoTotal.ElapsedMilliseconds, tempoDetran.ElapsedMilliseconds);
        }

        public async Task<(List<AulaPratica> Aulas, long TempoTotal, long TempoDetran)> ListarAulasPraticas(string renach)
        {
            var tempoTotal = Stopwatch.StartNew();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://www2.detran.rj.gov.br/portal/habilitacao/biometriaValid")
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    ["renach"] = renach,
                    ["disciplina"] = "TEORICAS",
                    ["tipo"] = "pratica"
                })
            };

            var tempoDetran = Stopwatch.StartNew();
            var html = await (await this.http.SendAsync(request)).Content.ReadAsStringAsync();
            tempoDetran.Stop();

            var document = new HtmlDocument();
            document.LoadHtml(html);

            var aulas = new List<AulaPratica>();

            foreach (var linha in document.QuerySelectorAll("tr").Skip(1))
            {
                var dados = linha.ChildNodes.Where(s => s.Name == "td").Select(s => s.InnerText).ToArray();

                var sdata = dados[0];
                var sinicio = dados[1];
                var sfim = dados[2];
                var sdisciplina = dados[3];
                var senvio = dados[4];
                var sstatus = dados[5];

                var data = DetranDate(sdata);
                var inicio = DetranTime(sinicio, false);
                var fim = DetranTime(sfim, false);
                var envio = DetranDate(senvio);
                var disciplina = sdisciplina;

                aulas.Add(new AulaPratica
                {
                    Data = new DateTime(data.Year, data.Month, data.Day, 0, 0, 0),
                    DataEnvio = envio,
                    Inicio = inicio,
                    Fim = fim,
                    Disciplina = disciplina,
                    Status = sstatus
                });
            }

            tempoTotal.Stop();

            return (aulas, tempoTotal.ElapsedMilliseconds, tempoDetran.ElapsedMilliseconds);
        }

        private static DateTime DetranDate(string v)
        {
            var partes = v.Split(' ');
            var partesData = partes[0].Split('/');

            var dia = int.Parse(partesData[1]);
            var mes = int.Parse(partesData[0]);
            var ano = int.Parse(partesData[2]);

            var t = DetranTime(string.Join(' ', partes.Skip(1)), true);
            var (hora, minuto, segundo) = (t.Hours, t.Minutes, t.Seconds);

            return new DateTime(ano, mes, dia, hora, minuto, segundo);
        }

        private static TimeSpan DetranTime(string v, bool time24)
        {
            var partes = v.Split(' ');
            var tpartes = partes[0].Split(':');

            var hora = int.Parse(tpartes[0]);
            var minuto = int.Parse(tpartes[1]);
            var segundo = int.Parse(tpartes[2]);

            if (time24 && partes[1] == "PM")
                hora += 12;

            return new TimeSpan(hora, minuto, segundo);
        }

        private static Disciplina DetranDis(string v)
        {
            return v switch
            {
                "DIREÇÃO DEFENSIVA" => Disciplina.DirecaoDefensiva,
                "PRIMEIROS SOCORROS" => Disciplina.PrimeirosSocorros,
                "LEGISLAÇÃO DE TRÂNSITO" => Disciplina.Legislacao,
                "NOÇÕES DE MECÂNICA VEICULAR" => Disciplina.Mecanica,
                "MEIO AMBIENTE CIDADANIA" => Disciplina.MeioAmbiente,
                _ => throw new Exception()
            };
        }
    }
}