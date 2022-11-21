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
            int winner = -1;
            do
            {
                #region Each Loop 1 User While Game Is Active
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
                    if (i == 1 || (i == 2 && mode == '2'))
                    {
                        #region Humanoids Go Here
                        for (int t = 0; t < 2; t++)
                        {
                            switch (t)
                            {
                                case 0:
                                    Console.Write("\n{0,36}", "Select Column");
                                    y = Input() - 1;
                                    break;
                                case 1:
                                    ClearLine();
                                    Console.WriteLine("{0,35}", "Select Row");
                                    x = Input() - 1;
                                    break;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region Bots Go Here
                        Random rnd = new();
                        x = rnd.Next(0, 3);
                        y = rnd.Next(0, 3);
                        #endregion
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
                        if (i == 1 || (i == 2 && mode == '2'))
                        {
                            App.Error(1800);
                        }
                        i--;
                    }
                    #endregion

                    #region Check Board
                    if (CheckTie())
                    {
                        gameOngoing = !CheckTie();
                        winner = 3;
                        break;
                    }
                    else if (CheckWinner())
                    {
                        gameOngoing = !CheckWinner();
                        if (i == 1)
                        {
                            winner = 1;
                            break;
                        }
                        else if (i == 2)
                        {
                            winner = 2;
                            break;
                        }
                    }
                    #endregion
                }
                #endregion

            } while (gameOngoing);

            GetWinner(winner);
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
                Console.Write("{0,27}", '|');
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
        static void GetWinner(int winner)
        {
            Console.Clear();
            Console.WriteLine(Banner.Result);

            GetField();
            Console.WriteLine(Environment.NewLine);

            if (winner == 3)
            {
                Console.WriteLine("Looks Like We Have A TIE");
            }
            else
            {
                Console.WriteLine($"Player {winner}, Also Known As {{{Players[winner - 1].Name}}} Won This Match");
            }
            Settings.Default.GamesPlayed++;
            Settings.Default.Save();
            Thread.Sleep(3600);
        }
        static bool CheckTie()
        {
            for (int y = 0; y < Field.GetLength(0); y++)
            {
                for (int x = 0; x < Field.GetLength(1); x++)
                {
                    if (Field[y, x] == ' ')
                    {
                        return false; // If There Is One Space Free NO TIE
                    }
                }
            }
            return true; // TIE
        }
        static bool CheckWinner()
        {
            if (HorizontalCheck())
            {
                return true;
            }
            if (VerticalCheck())
            {
                return true;
            }
            if (DiagonalCheck())
            {
                return true;
            }
            return false; // If We Haven't Returned Outta Here It Can't Be Real ❗😭🤓 Can It ❔

        }
        static bool HorizontalCheck()
        {
            if ((Field[0, 0] == Field[0, 1] && Field[0, 1] == Field[0, 2])
                && (Field[0, 0] != ' ' && Field[0, 1] != ' ' && Field[0, 2] != ' '))
            {
                return true;
            }
            else if ((Field[1, 0] == Field[1, 1] && Field[1, 1] == Field[1, 2])
                && (Field[1, 0] != ' ' && Field[1, 1] != ' ' && Field[1, 2] != ' '))
            {
                return true;
            }
            else if ((Field[2, 0] == Field[2, 1] && Field[2, 1] == Field[2, 2])
                && (Field[2, 0] != ' ' && Field[2, 1] != ' ' && Field[2, 2] != ' '))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static bool VerticalCheck()
        {
            if ((Field[0, 0] == Field[1, 0] && Field[1, 0] == Field[2, 0])
                && (Field[0, 0] != ' ' && Field[1, 0] != ' ' && Field[2, 0] != ' '))
            {
                return true;
            }
            else if ((Field[0, 1] == Field[1, 1] && Field[1, 1] == Field[2, 1])
                && (Field[0, 1] != ' ' && Field[1, 1] != ' ' && Field[2, 1] != ' '))
            {
                return true;
            }
            else if ((Field[0, 2] == Field[1, 2] && Field[1, 2] == Field[2, 2])
                && (Field[0, 2] != ' ' && Field[1, 2] != ' ' && Field[2, 2] != ' '))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static bool DiagonalCheck()
        {
            if ((Field[0, 0] == Field[1, 1] && Field[1, 1] == Field[2, 2])
                && (Field[0, 0] != ' ' && Field[1, 1] != ' ' && Field[2, 2] != ' '))
            {
                return true;
            }
            else if ((Field[0, 2] == Field[1, 1] && Field[1, 1] == Field[2, 0])
                && (Field[0, 2] != ' ' && Field[1, 1] != ' ' && Field[2, 0] != ' '))
            {
                return true;
            }
            else
            {
                return false;
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
        static void ClearLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
        #endregion
    }
}
