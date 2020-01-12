using System;
using tabuleiro;
using regras;

namespace Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            PosicaoXadrez pos = new PosicaoXadrez(7, 'c');
            Console.WriteLine(pos.toPosicao());
        }
    }
}
