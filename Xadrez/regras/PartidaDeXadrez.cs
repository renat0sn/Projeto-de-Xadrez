using System.Collections.Generic;
using tabuleiro;
using regras;

namespace regras
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        private HashSet<Peca> Pecas;
        private HashSet<Peca> Capturadas;
        public bool Xeque { get; private set; }

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branco;
            Terminada = false;
            Xeque = false;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            InserirPecas();
        }

        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            p.IncrementarNumMovimentos();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.InserirPeca(p, destino);
            if (pecaCapturada != null)
            {
                Capturadas.Add(pecaCapturada);
            }

            //ROQUE pequeno
            if(p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca t = Tab.RetirarPeca(origemT);
                t.IncrementarNumMovimentos();
                Tab.InserirPeca(t, destinoT);
            }

            //ROQUE grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca t = Tab.RetirarPeca(origemT);
                t.IncrementarNumMovimentos();
                Tab.InserirPeca(t, destinoT);
            }
            return pecaCapturada;
        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = Tab.RetirarPeca(destino);
            p.DecrementarNumMovimentos();
            if (pecaCapturada != null)
            {
                Tab.InserirPeca(pecaCapturada, destino);
                Capturadas.Remove(pecaCapturada);
            }
            Tab.InserirPeca(p, origem);

            //ROQUE pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca t = Tab.RetirarPeca(destinoT);
                t.DecrementarNumMovimentos();
                Tab.InserirPeca(t, origemT);
            }
            //ROQUE grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca t = Tab.RetirarPeca(destinoT);
                t.DecrementarNumMovimentos();
                Tab.InserirPeca(t, origemT);
            }
        }
        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);

            if (EstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            if (EstaEmXeque(Adversaria(JogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }

            if (TesteXequeMate(Adversaria(JogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                MudaJogador();
            }
        }

        public void ValidarPosicaoDeOrigem(Posicao pos)
        {
            if (Tab.Peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if (JogadorAtual != Tab.Peca(pos).Cor)
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
            if (!Tab.Peca(origem).MovimentoPossivel(destino))
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }
        private void MudaJogador()
        {
            if (JogadorAtual == Cor.Branco)
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
            foreach (Peca x in Capturadas)
            {
                if (x.Cor == cor)
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

        private Cor Adversaria(Cor cor)
        {
            if (cor == Cor.Branco)
            {
                return Cor.Preto;
            }
            else
            {
                return Cor.Branco;
            }
        }

        private Peca Rei(Cor cor)
        {
            foreach (Peca x in PecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool EstaEmXeque(Cor cor)
        {
            Peca R = Rei(cor);
            if (R == null)
            {
                throw new TabuleiroException("Não tem rei da cor " + cor + " no tabuleiro!");
            }

            foreach (Peca x in PecasEmJogo(Adversaria(cor)))
            {
                if (x.MovimentoPossivel(R.Posicao))
                {
                    return true;
                }
            }
            return false;
        }

        public bool TesteXequeMate(Cor cor)
        {
            if (!EstaEmXeque(cor))
            {
                return false;
            }
            foreach(Peca x in PecasEmJogo(cor))
            {
                bool[,] mat = x.MovimentosPossiveis();
                for (int i=0; i<Tab.Linhas; i++)
                {
                    for(int j=0;j<Tab.Colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutaMovimento(origem, destino);
                            bool testeXeque = EstaEmXeque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
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
            ColocarNovaPeca(1, 'b', new Cavalo(Cor.Branco, Tab));
            ColocarNovaPeca(1, 'g', new Cavalo(Cor.Branco, Tab));
            ColocarNovaPeca(1, 'c', new Bispo(Cor.Branco, Tab));
            ColocarNovaPeca(1, 'f', new Bispo(Cor.Branco, Tab));
            ColocarNovaPeca(1, 'd', new Dama(Cor.Branco, Tab));
            ColocarNovaPeca(1, 'e', new Rei(Cor.Branco, Tab, this));
            ColocarNovaPeca(2, 'a', new Peao(Cor.Branco, Tab));
            ColocarNovaPeca(2, 'b', new Peao(Cor.Branco, Tab));
            ColocarNovaPeca(2, 'c', new Peao(Cor.Branco, Tab));
            ColocarNovaPeca(2, 'd', new Peao(Cor.Branco, Tab));
            ColocarNovaPeca(2, 'e', new Peao(Cor.Branco, Tab));
            ColocarNovaPeca(2, 'f', new Peao(Cor.Branco, Tab));
            ColocarNovaPeca(2, 'g', new Peao(Cor.Branco, Tab));
            ColocarNovaPeca(2, 'h', new Peao(Cor.Branco, Tab));
            ColocarNovaPeca(8, 'a', new Torre(Cor.Preto, Tab));
            ColocarNovaPeca(8, 'h', new Torre(Cor.Preto, Tab));
            ColocarNovaPeca(8, 'e', new Rei(Cor.Preto, Tab, this));
            ColocarNovaPeca(8, 'b', new Cavalo(Cor.Preto, Tab));
            ColocarNovaPeca(8, 'g', new Cavalo(Cor.Preto, Tab));
            ColocarNovaPeca(8, 'c', new Bispo(Cor.Preto, Tab));
            ColocarNovaPeca(8, 'f', new Bispo(Cor.Preto, Tab));
            ColocarNovaPeca(8, 'd', new Dama(Cor.Preto, Tab));
            ColocarNovaPeca(7, 'a', new Peao(Cor.Preto, Tab));
            ColocarNovaPeca(7, 'b', new Peao(Cor.Preto, Tab));
            ColocarNovaPeca(7, 'c', new Peao(Cor.Preto, Tab));
            ColocarNovaPeca(7, 'd', new Peao(Cor.Preto, Tab));
            ColocarNovaPeca(7, 'e', new Peao(Cor.Preto, Tab));
            ColocarNovaPeca(7, 'f', new Peao(Cor.Preto, Tab));
            ColocarNovaPeca(7, 'g', new Peao(Cor.Preto, Tab));
            ColocarNovaPeca(7, 'h', new Peao(Cor.Preto, Tab));
        }
    }
}
