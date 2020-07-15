using DetranConsulta.Detran.Model;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DetranConsulta.Detran.Parser
{
    public abstract class AulaParser<T> : IParser<List<T>> where T : Aula, new()
    {
        public List<T> Parse(HtmlDocument html)
        {
            var aulas = new List<T>();

            foreach (var linha in html.QuerySelectorAll("tr").Skip(1))
            {
                var dados = linha.ChildNodes.Where(s => s.Name == "td").Select(s => s.InnerText).ToArray();

                var aula = new T
                {
                    Data = this.ParseDate(dados[0]).Date,
                    DataEnvio = this.ParseDate(dados[4]),
                    Inicio = this.ParseTime(dados[1]),
                    Fim = this.ParseTime(dados[2]),
                    Status = dados[5]
                };

                this.ParseAula(aula, dados);

                aulas.Add(aula);
            }

            return aulas;
        }

        protected abstract void ParseAula(T aula, string[] dados);

        private DateTime ParseDate(string value)
        {
            if (DateTime.TryParseExact(value, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out var data))
                return data;
            else
                throw new Exception($"Não foi possível fazer o parse da data '{value}'.");
        }

        private TimeSpan ParseTime(string value)
        {
            if (TimeSpan.TryParseExact(value, "hh':'mm':'ss", CultureInfo.InvariantCulture, TimeSpanStyles.None, out var time))
                return time;
            else
                throw new Exception($"Não foi possível fazer o parse do tempo '{value}'.");
        }
    }
}