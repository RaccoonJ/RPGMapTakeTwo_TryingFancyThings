using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Cryptography.X509Certificates;

namespace _2DRPGMap
{
    internal class Program
    {
        static int Scale;
        static int origX, origY;
        static string Player;
        static int Rows, Columns;
        static ConsoleKeyInfo key;
        static bool GameOver;
        static int PlayerPosx, PlayerPosy;
        static bool moveRollBack;


        static char[,] map = new char[,]
    {
        {'^','^','^','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`'},
        {'^','^','`','`','`','`','*','*','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','~','~','~','`','`','`'},
        {'^','^','`','`','`','*','*','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','~','~','~','`','`','`','`','`'},
        {'^','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`'},
        {'`','`','`','`','~','~','~','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`'},
        {'`','`','`','`','~','~','~','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`'},
        {'`','`','`','~','~','~','~','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','^','^','`','`','`','`','`','`'},
        {'`','`','`','`','`','~','~','~','`','`','`','`','`','`','`','`','`','`','`','`','`','^','^','^','^','`','`','`','`','`'},
        {'`','`','`','`','`','~','~','~','~','`','`','`','`','`','`','`','`','`','`','`','`','`','`','^','^','^','^','`','`','`'},
        {'`','`','`','`','`','`','`','~','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`'},
        {'`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`'},
        {'`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`'},
    };


        static void Main(string[] args)
        {
            Scale = 1;
            origX = Console.CursorLeft;
            origY = Console.CursorTop;
            PlayerPosy = origY + 4 * Scale;
            PlayerPosx = origX + 8 * Scale;
            Player = "0";
            GameOver = false;
            Rows = map.GetLength(0);
            Columns = map.GetLength(1);
            moveRollBack = false;


            while (GameOver == false)
            {
                Console.Clear();
                DisplayMap(Scale);
                PlayerDraw(Player, PlayerPosx, PlayerPosy);
                Console.WriteLine();
                Console.SetCursorPosition(0, Rows * Scale + 2);
                PlayerChoice();
                PlayerDraw(Player, PlayerPosx, PlayerPosy);

            }
        }
        static void DisplayMap()
        {
            Console.BackgroundColor = ConsoleColor.Black;

            for (int x = 0; x < Rows; x++)
            {
                Console.Write("#");
                for (int y = 0; y < Columns; y++)
                {
                    Console.Write(map[x, y]);
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("#");
                Console.WriteLine();
            }

            Console.ResetColor();
        }
        static void DisplayMap(int scale)
        {
            int bordersize = Columns * scale;
            Console.BackgroundColor = ConsoleColor.Black;

            for (int g = 0; g < 1; g++)
            {
                Console.Write("█");
                for (int r = 0; r < bordersize; r++)
                {
                    Console.Write("█");
                }
                Console.Write("█");
            }

            Console.WriteLine();

            for (int x = 0; x < Rows; x++)
            {
                for (int m = 0; m < scale; m++)
                {
                    Console.Write("█");
                    for (int y = 0; y < Columns; y++)
                    {
                        for (int z = 0; z < scale; z++)
                        {
                            Console.Write(map[x, y]);
                        }
                    }
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("█");
                    Console.WriteLine();
                }
            }
            for (int g = 0; g < 1; g++)
            {
                Console.Write("█");
                for (int r = 0; r < bordersize; r++)
                {
                    Console.Write("█");
                }
                Console.Write("█");
            }
            Console.WriteLine();
        }

        static void PlayerChoice()
        {
            Console.WriteLine();


            key = Console.ReadKey();
            if (key.Key == ConsoleKey.Escape)
            {
                GameOver = true;
            }
            else if (key.Key == ConsoleKey.W)
            {
                PlayerPosy--;
                Console.SetCursorPosition(0, Rows * Scale + 2);
                WallCheck(PlayerPosx, PlayerPosy);
                if (moveRollBack == true)
                {
                    moveRollBack = false;
                    PlayerPosy++;
                }
            }
            else if (key.Key == ConsoleKey.A)
            {
                PlayerPosx--;
                WallCheck(PlayerPosx, PlayerPosy);
                if (moveRollBack == true)
                {
                    moveRollBack = false;
                    PlayerPosx++;
                }
            }
            else if (key.Key == ConsoleKey.S)
            {
                PlayerPosy++;
                WallCheck(PlayerPosx, PlayerPosy);
                if (moveRollBack == true)
                {
                    moveRollBack = false;
                    PlayerPosy--;
                }
            }
            else if (key.Key == ConsoleKey.D)
            {
                PlayerPosx++;
                WallCheck(PlayerPosx, PlayerPosy);
                if (moveRollBack == true)
                {
                    moveRollBack = false;
                    PlayerPosx--;
                }
            }
            else if (key.Key == ConsoleKey.E)
            {
                Scale++;
            }
        }
        static void PlayerDraw(string Player, int PlayerPosx, int PlayerPosy)
        {
            Console.SetCursorPosition(origX + PlayerPosx, origY + PlayerPosy);
            Console.Write(Player);
        }
        static void WallCheck(int x, int y)
        {
            if (x > Columns || x < 0 + 1)
            {
                moveRollBack = true;
            }
            else if (y > Rows || y < 0 + 1)
            {
                moveRollBack = true;
            }

            else
            {
                switch (map[y - 1, x - 1])
                {
                    case '~':
                        moveRollBack = true;
                        break;
                    case '^':
                        moveRollBack = true;
                        break;
                    default:
                        moveRollBack = false;
                        break;
                }
            }
        }
    }
}