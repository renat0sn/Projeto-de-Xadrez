using System;
using tabuleiro;
using regras;

namespace Xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
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

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            ExecutaMovimento(origem, destino);
            Turno++;
            MudaJogador();
        }

        public void ValidarPosicaoDeOrigem(Posicao pos)
        {
            if(Tab.Peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if(JogadorAtual != Tab.Peca(pos).Cor)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            if (!Tab.Peca(pos).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.Peca(origem).PodeMoverPara(destino))
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }
        private void MudaJogador()
        {
            if(JogadorAtual == Cor.Branco)
            {
                JogadorAtual = Cor.Preto;
            }
            else
            {
                JogadorAtual = Cor.Branco;
            }
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
