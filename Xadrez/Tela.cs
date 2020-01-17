using System.Collections.Generic;
using System;
using tabuleiro;
using regras;

namespace Xadrez
{
    class Tela
    {
        public static void ImprimirPartida(PartidaDeXadrez partida)
        {
            ImprimirTabuleiro(partida.Tab);
            Console.WriteLine();
            ImprimirPecasCapturadas(partida);
            Console.WriteLine("\nTurno: " + partida.Turno);
            Console.WriteLine("Aguardando jogada: " + partida.JogadorAtual);
        }

        public static void ImprimirPecasCapturadas(PartidaDeXadrez partida)
        {
            Console.Write("Peças capturadas -> ");
            Console.Write("Brancas: ");
            ImprimirConjunto(partida.PecasCapturadas(Cor.Branco));
            Console.Write("  Pretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            ImprimirConjunto(partida.PecasCapturadas(Cor.Preto));
            Console.ForegroundColor = aux;
        }

        public static void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach(Peca x in conjunto)
            {
                Console.Write(x + " ");
            }
            Console.Write("]");
        }
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(" " + (8 - i) + " ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    ImprimirPeca(tab.Peca(i, j));
                }
                if (i != 7)
                {
                    Console.WriteLine("\n\n");
                }
            }

            Console.Write("\n\n      ");
            Console.WriteLine("a      b      c      d      e      f      g      h\n");
        }

        public static void ImprimirTabuleiro(Tabuleiro tab, bool[,] posicoesPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(" " + (8 - i) + " ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (posicoesPossiveis[i, j])
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }
                    ImprimirPeca(tab.Peca(i, j));
                    Console.BackgroundColor = fundoOriginal;
                }
                if (i != 7)
                {
                    Console.WriteLine("\n\n");
                }
            }

            Console.Write("\n\n      ");
            Console.WriteLine("a      b      c      d      e      f      g      h\n");
        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(linha, coluna);
        }
        public static void ImprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("   -   ");
            }
            else
            {
                if (peca.Cor == Cor.Branco)
                {
                    Console.Write(peca);
                }

                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }


            }
        }
    }
}
