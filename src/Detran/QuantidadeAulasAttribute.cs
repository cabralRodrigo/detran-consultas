using System;

namespace DetranConsulta.Detran
{
    public class QuantidadeAulasAttribute : Attribute
    {
        public int Quantidade { get; }

        public QuantidadeAulasAttribute(int quantidade)
        {
            this.Quantidade = quantidade;
        }

    }
}