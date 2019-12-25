using System;
using tabuleiro;
using regras;

namespace Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Posicao p = new Posicao(3, 4);
                Tabuleiro t = new Tabuleiro(8, 8);
                t.InserirPeca(new Cavalo(Cor.Preto, t), new Posicao(5, 5));
                t.InserirPeca(new Rei(Cor.Preto, t), new Posicao(5, 4));
                t.InserirPeca(new Torre(Cor.Preto, t), new Posicao(5, 5));
                t.InserirPeca(new Rei(Cor.Preto, t), new Posicao(4, 7));
                Tela.ImprimirTabuleiro(t);
            }

            catch(TabuleiroException e)
            {
                Console.WriteLine("Erro de tabuleiro: " + e.Message);
            }
        }
    }
}
