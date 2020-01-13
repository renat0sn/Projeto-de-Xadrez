using System;
using tabuleiro;
using regras;

namespace Xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; private set; }
        private int Turno;
        private Cor JogadorAtual;
        public bool Terminada { get; private set; }

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branco;
            Terminada = false;
            InserirPecas();
        }

        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            p.IncrementarNumMovimentos();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.InserirPeca(p, destino);
        }

        private void InserirPecas()
        {
            Tab.InserirPeca(new Torre(Cor.Branco, Tab), new PosicaoXadrez(1, 'a').toPosicao());
            Tab.InserirPeca(new Torre(Cor.Branco, Tab), new PosicaoXadrez(1, 'h').toPosicao());
            Tab.InserirPeca(new Rei(Cor.Branco, Tab), new PosicaoXadrez(1, 'd').toPosicao());
            Tab.InserirPeca(new Cavalo(Cor.Branco, Tab), new PosicaoXadrez(1, 'b').toPosicao());
            Tab.InserirPeca(new Cavalo(Cor.Branco, Tab), new PosicaoXadrez(1, 'g').toPosicao());
            Tab.InserirPeca(new Torre(Cor.Amarelo, Tab), new PosicaoXadrez(8, 'a').toPosicao());
            Tab.InserirPeca(new Torre(Cor.Amarelo, Tab), new PosicaoXadrez(8, 'h').toPosicao());
            Tab.InserirPeca(new Rei(Cor.Amarelo, Tab), new PosicaoXadrez(8, 'e').toPosicao());
            Tab.InserirPeca(new Cavalo(Cor.Amarelo, Tab), new PosicaoXadrez(8, 'b').toPosicao());
            Tab.InserirPeca(new Cavalo(Cor.Amarelo, Tab), new PosicaoXadrez(8, 'g').toPosicao());
        }
    }
}
