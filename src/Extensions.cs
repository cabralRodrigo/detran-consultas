using DetranConsulta.Detran;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace DetranConsulta
{
    public static class Extensions
    {
        public static string Name<T>(this T source) where T : Enum
        {
            return typeof(T).GetMember(source.ToString()).Single().GetCustomAttribute<DescriptionAttribute>()?.Description ?? source.ToString();
        }

        public static int? QuantidadeAulas<T>(this T source) where T : Enum
        {
            return typeof(T).GetMember(source.ToString()).Single().GetCustomAttribute<QuantidadeAulasAttribute>()?.Quantidade;
        }
    }
}