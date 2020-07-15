using HtmlAgilityPack;

namespace DetranConsulta.Detran.Parser
{
    public interface IParser<T>
    {
        T Parse(HtmlDocument html);
    }
}