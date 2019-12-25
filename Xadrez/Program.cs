using System;
using tabuleiro;

namespace Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            Posicao p = new Posicao(3,4);
            Tabuleiro t = new Tabuleiro(8, 8); 
            Console.WriteLine(p);
            Console.ReadLine();
        }
    }
}
