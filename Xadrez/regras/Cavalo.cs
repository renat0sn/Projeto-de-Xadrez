using System;
using tabuleiro;

namespace regras
{
    class Cavalo : Peca
    {
        public Cavalo(Cor cor, Tabuleiro tab) : base(cor, tab)
        {
        }

        public override string ToString()
        {
            return " Cavalo";
        }
    }
}
