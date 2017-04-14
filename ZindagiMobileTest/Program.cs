using System;
using System.Collections.Generic;

namespace ZindagiMobileTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // 0 == empty board spot
            // 1 == valid piece
            int[][] boardLayout = 
            {
                new int[] {0,0,0,1,1,1,1,1},
                new int[] {0,0,0,1,1,1,1,1},
                new int[] {0,0,1,1,1,1,1,1,1},
                new int[] {1,1,1,1,1,1,1,1,1,1,1},
                new int[] {1,1,1,1,1,1,1,1,1,1,1},
                new int[] {1,1,1,1,1,0,1,1,1,1,1},
                new int[] {1,1,1,1,1,1,1,1,1,1,1},
                new int[] {1,1,1,1,1,1,1,1,1,1,1},
                new int[] {0,0,1,1,1,1,1,1,1},
                new int[] {0,0,0,1,1,1,1,1},
                new int[] {0,0,0,1,1,1,1,1},
            };
            
            // piece set
            PieceInfo[] pieces = 
            {  
                new PieceInfo('A', ConsoleColor.Blue),
                new PieceInfo('S', ConsoleColor.Red),
                new PieceInfo('D', ConsoleColor.Yellow),
                new PieceInfo('F', ConsoleColor.Green)
            };

            // new game
            Game game = new Game(boardLayout, pieces);

            // main loop
            for (; ; )
            {
                game.FindBlocks();
                game.Print();

                ConsoleKeyInfo ck = Console.ReadKey();
                if (ck.Key == ConsoleKey.Escape)
                    break;

                game.ProcessInput(ck.KeyChar);

             //   Console.Clear();
            }
        }
    }

    class PieceInfo
    {
        public PieceInfo(char t, ConsoleColor c)
        {
            type = t;
            highlightColor = c;
        }

        public char type;
        public ConsoleColor highlightColor;
    }
}
