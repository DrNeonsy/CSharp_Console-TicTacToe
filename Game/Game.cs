using System.Drawing.Printing;

namespace TicTacToe
{
    internal class Game
    {
        #region Fields And Properties
        static Players[] Players { get; set; } = new Players[2];
        static char[,] Field { get; set; } = new char[3, 3];
        #endregion

        #region Methods
        internal static void Play(char mode)
        {
            SetPlayers();

        }
        static void SetPlayers()
        {
            for (int i = 0; i < 2; i++)
            {
                Players[i] = new Players(i + 1, SetName(i + 1));
            }
            static string SetName(int player)
            {
                Console.CursorVisible = true;

                while (true)
                {
                    Console.Clear();
                    Console.Write($"Enter Player {player} Name: ");

                    string? input = Console.ReadLine();

                    if (!string.IsNullOrEmpty(input))
                    {
                        if (input.Length >= 3 && input.Length <= 18)
                        {
                            Console.CursorVisible = false;
                            Console.Clear();
                            return _ = char.ToUpper(input[0]) + input[1..]; // Substring(1 - X)
                        }
                    }
                    InvalidInput();
                }

                static void InvalidInput()
                {
                    Console.Clear();
                    Console.WriteLine(Banner.Invalid);
                    Console.WriteLine("Make Sure That Your Input Is Between 3 And 18 Chars");

                    Thread.Sleep(2700);
                }
            }
        }
        static void SetField()
        {
            Field[0, 0] = Players[0].Symbole;
            for (int y = 0; y < Field.GetLength(0); y++)
            {
                for (int x = 0; x < Field.GetLength(1); x++)
                {
                    if (x > 0)
                    {
                        if (Field[y, x - 1] == Players[0].Symbole)
                        {
                            Field[y, x] = Players[1].Symbole;
                        }
                        else if (Field[y, x - 1] == Players[1].Symbole) // Being Specific For Easy Bug Detection
                        {
                            Field[y, x] = Players[0].Symbole;
                        }
                    }
                    else if (x == 0 && y > 0)
                    {
                        if (Field[y - 1, 2] == Players[0].Symbole)
                        {
                            Field[y, x] = Players[1].Symbole;
                        }
                        else if (Field[y - 1, 2] == Players[1].Symbole)
                        {
                            Field[y, x] = Players[0].Symbole;
                        }
                    }
                }
            }
        }
        static void GetField()
        {
            for (int y = 0; y < Field.GetLength(0); y++)
            {
                Console.Write('|');
                for (int x = 0; x < Field.GetLength(1); x++)
                {
                    if (Field[y, x] == Players[0].Symbole)
                    {
                        Console.ForegroundColor = Players[0].Color;
                    }
                    else if (Field[y, x] == Players[1].Symbole)
                    {
                        Console.ForegroundColor = Players[1].Color;
                    }

                    Console.Write(Field[y, x]);

                    Console.ResetColor();
                    Console.Write('|');
                }
                Console.WriteLine();
            }
        }
        #endregion
    }
}
