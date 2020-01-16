

namespace tabuleiro
{
    abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int NumMovimentos { get; protected set; }
        public Tabuleiro Tab { get; protected set; }

        public Peca(Cor cor, Tabuleiro tab)
        {
            NumMovimentos = 0;
            Posicao = null;
            Cor = cor;
            Tab = tab;
        }

        public void IncrementarNumMovimentos()
        {
            NumMovimentos++;
        }

        public abstract bool[,] MovimentosPossiveis();
    }
}
