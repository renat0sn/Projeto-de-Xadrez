using tabuleiro;

namespace regras
{
    class Cavalo : Peca
    {
        public Cavalo(Cor cor, Tabuleiro tab) : base(cor, tab)
        {
        }

        public override bool[,] MovimentosPossiveis()
        {
            throw new System.NotImplementedException();
        }
        public override string ToString()
        {
            return " Cavalo";
        }
    }
}
