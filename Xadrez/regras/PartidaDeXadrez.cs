using System.Collections.Generic;
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
        private HashSet<Peca> Pecas;
        private HashSet<Peca> Capturadas;

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branco;
            Terminada = false;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            InserirPecas();
        }

        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            p.IncrementarNumMovimentos();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.InserirPeca(p, destino);
            if(pecaCapturada != null)
            {
                Capturadas.Add(pecaCapturada);
            }
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

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in Capturadas)
            {
                if(x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in Pecas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        public void ColocarNovaPeca(int linha, char coluna, Peca peca)
        {
            Tab.InserirPeca(peca, new PosicaoXadrez(linha, coluna).toPosicao());
            Pecas.Add(peca);
        }
        private void InserirPecas()
        {
            ColocarNovaPeca(1, 'a', new Torre(Cor.Branco, Tab));
            ColocarNovaPeca(1, 'h', new Torre(Cor.Branco, Tab));
            ColocarNovaPeca(1, 'd', new Rei(Cor.Branco, Tab));
            ColocarNovaPeca(1, 'b', new Cavalo(Cor.Branco, Tab));
            ColocarNovaPeca(1, 'g', new Cavalo(Cor.Branco, Tab));
            ColocarNovaPeca(8, 'a', new Torre(Cor.Preto, Tab));
            ColocarNovaPeca(8, 'h', new Torre(Cor.Preto, Tab));
            ColocarNovaPeca(8, 'e', new Rei(Cor.Preto, Tab));
            ColocarNovaPeca(8, 'b', new Cavalo(Cor.Preto, Tab));
            ColocarNovaPeca(8, 'g', new Cavalo(Cor.Preto, Tab));
        }
    }
}
