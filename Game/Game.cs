namespace TicTacToe
{
    internal class Game
    {
        #region Fields And Properties
        static Players[] Players { get; set; } = new Players[2];
        static char[,] Field { get; set; } = new char[3, 3];
        static char[] Pos { get; set; } = { '1', '2', '3' };
        #endregion

        #region Methods
        internal static void Start(char mode)
        {
            SetPlayers();
            SetField();
            Play(mode);
        }
        static void Play(char mode)
        {
            bool gameOngoing = true;
            do
            {
                #region Player Based Loop I/O
                for (int i = 1; i <= 2; i++)
                {
                    Console.Clear();
                    #region Output
                    switch (i)
                    {
                        case 1:
                            Console.WriteLine(Banner.P1);
                            break;
                        case 2:
                            Console.WriteLine(Banner.P2);
                            break;
                    }
                    GetField();
                    #endregion

                    #region Input
                    int y = -1, x = -1;
                    for (int t = 0; t < 2; t++)
                    {
                        switch (t)
                        {
                            case 0:
                                Console.Write(Environment.NewLine + "Select Row");
                                x = Input() - 1;
                                break;
                            case 1:
                                Console.WriteLine("\rSelect Column");
                                y = Input() - 1;
                                break;
                        }
                    }
                    #endregion

                    #region SetSymbole
                    if (Field[y, x] == ' ')
                    {
                        switch (i)
                        {
                            case 1:
                                Field[y, x] = Players[0].Symbole;
                                break;
                            case 2:
                                Field[y, x] = Players[1].Symbole;
                                break;
                        }
                    }
                    else
                    {
                        App.Error(1800);
                        i--;
                    }
                    #endregion
                }
                #endregion

            } while (gameOngoing);
        }
        static void SetPlayers()
        {
            for (int i = 0; i < 2; i++)
            {
                Players[i] = new Players(i + 1, SetName(i + 1));
            }
            static string SetName(int player)
            {
                while (true)
                {
                    Console.Clear();
                    Console.CursorVisible = true;
                    Console.WriteLine(Banner.Setup);
                    Console.Write($"Enter Player {player} Name: ");

                    string? input = Console.ReadLine();

                    if (!string.IsNullOrEmpty(input))
                    {
                        if (input.Length >= 3 && input.Length <= 18 && input.All(char.IsLetterOrDigit))
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
                    Console.CursorVisible = false;
                    Console.WriteLine(Banner.Invalid);
                    Console.WriteLine("Length: Min 3 Max 18 | No Special Chars");

                    Thread.Sleep(2700);
                }
            }
        }
        static void SetField()
        {
            for (int y = 0; y < Field.GetLength(0); y++)
            {
                for (int x = 0; x < Field.GetLength(1); x++)
                {
                    Field[y, x] = ' ';
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
        static int Input()
        {
            ConsoleKeyInfo key;
            bool success = false;

            do
            {
                key = Console.ReadKey(true);

                foreach (char option in Pos)
                {
                    if (option == key.KeyChar)
                    {
                        success = true;
                        break;
                    }
                }
            } while (!success);

            return Convert.ToInt32(char.GetNumericValue(key.KeyChar));
        }
        #endregion
    }
}
