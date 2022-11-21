using System.Media;

namespace TicTacToe
{
    internal class App
    {
        #region Music Object
        static readonly SoundPlayer music = new() // Creating An Object And Setting Sound Path
        {
            Stream = Banner.BG_Music
        };
        #endregion

        #region ValidMenuOptions
        static char[] MenuOptions { get; } = { '1', '2', '3', '4', '5', '6', '7' };
        #endregion

        #region Color List
        internal static Dictionary<int, ConsoleColor> PlayerColors { get; set; }
        = new()
        {
            {0 , ConsoleColor.Green},
            {1 , ConsoleColor.DarkGreen},
            {2 , ConsoleColor.Red},
            {3 , ConsoleColor.DarkRed},
            {4 , ConsoleColor.Cyan},
            {5 , ConsoleColor.DarkCyan},
            {6 , ConsoleColor.Blue},
            {7 , ConsoleColor.DarkBlue},
            {8 , ConsoleColor.Magenta},
            {9 , ConsoleColor.DarkMagenta}
        };
        #endregion

        #region Methods
        internal static void StartUp()
        {
            Config();
            Intro();
            MusicHandle();

            static void Config()
            {
                Console.CursorVisible = false;
                Console.Title = "Tic Tac Toe - Console Game";
            }
            static void Intro()
            {
                Console.WriteLine(Banner.Tic);
                Thread.Sleep(555);

                Console.WriteLine(Banner.Tac);
                Thread.Sleep(555);

                Console.WriteLine(Banner.Toe);
                Thread.Sleep(1000);
            }
        }

        internal static void Menu()
        {
            Console.Clear();

            RenderMenu();
            ExecuteMenu();

            static void RenderMenu()
            {
                MenuUpper();
                MenuLower();

                static void MenuUpper()
                {
                    #region Menu Point Variables (actionX)
                    string action1 = "SinglePlayer";
                    string action2 = "MultiPlayer";
                    string action3 = "Change Symbol";
                    string action4 = "Change Color";
                    string action5 = "Toggle Music";
                    string action6 = "Reset Stats";
                    string action7 = "Exit";
                    #endregion

                    Console.Write(Banner.Menu);

                    Console.WriteLine(new string('=', 36));
                    Console.WriteLine("{0,3}{1,-15}{2,15}{3,-2}{4}", "- ", action1, "| ", MenuOptions[0], '|');
                    Console.WriteLine("{0,3}{1,-15}{2,15}{3,-2}{4}", "- ", action2, "| ", MenuOptions[1], '|');
                    Console.WriteLine("{0,3}{1,-15}{2,15}{3,-2}{4}", "- ", action3, "| ", MenuOptions[2], '|');
                    Console.WriteLine("{0,3}{1,-15}{2,15}{3,-2}{4}", "- ", action4, "| ", MenuOptions[3], '|');
                    Console.WriteLine("{0,3}{1,-15}{2,15}{3,-2}{4}", "- ", action5, "| ", MenuOptions[4], '|');
                    Console.WriteLine("{0,3}{1,-15}{2,15}{3,-2}{4}", "- ", action6, "| ", MenuOptions[5], '|');
                    Console.WriteLine("{0,3}{1,-15}{2,15}{3,-2}{4}", "- ", action7, "| ", MenuOptions[6], '|');
                    Console.WriteLine(new string('=', 36));
                }
                static void MenuLower()
                {
                    #region Menu Point Variables (statX)
                    string stat1 = "Music";
                    string stat2 = "Games Played";
                    string stat3 = "Player 1 Symbol";
                    string stat4 = "Player 2 Symbol";
                    #endregion

                    #region Music Text
                    string musicState;

                    if (Settings.Default.MusicActive)
                    {
                        musicState = "Act";
                    }
                    else
                    {
                        musicState = "Off";
                    }
                    #endregion

                    Console.Write(Banner.Stats);

                    Console.WriteLine(new string('=', 36));
                    Console.WriteLine("{0,3}{1,-23}{2,5}{3,-4}{4}", "- ", stat1, "| ", musicState, '|');
                    Console.WriteLine("{0,3}{1,-23}{2,5}{3,2}{4,3}", "- ", stat2, "| ", Settings.Default.GamesPlayed, '|');

                    #region Player1Char
                    string player1Char = String.Format("{0,3}{1,-23}{2,5}{3,2}{4,3}", "- ", stat3, "| ", Settings.Default.Player1Char, '|');
                    for (int i = 0; i < player1Char.Length; i++)
                    {
                        bool colorChanged = false;
                        if (player1Char[i] == Settings.Default.Player1Char && player1Char.LastIndexOf(Settings.Default.Player1Char) == i)
                        {
                            Console.ForegroundColor = PlayerColors[Settings.Default.Player1Color];
                            colorChanged = true;
                        }
                        Console.Write(player1Char[i]);
                        if (colorChanged)
                        {
                            Console.ResetColor();
                        }
                    }
                    Console.WriteLine();
                    #endregion

                    #region Player2Char
                    string player2Char = String.Format("{0,3}{1,-23}{2,5}{3,2}{4,3}", "- ", stat4, "| ", Settings.Default.Player2Char, '|');
                    for (int i = 0; i < player1Char.Length; i++)
                    {
                        bool colorChanged = false;
                        if (player2Char[i] == Settings.Default.Player2Char && player2Char.LastIndexOf(Settings.Default.Player2Char) == i)
                        {
                            Console.ForegroundColor = PlayerColors[Settings.Default.Player2Color];
                            colorChanged = true;
                        }
                        Console.Write(player2Char[i]);
                        if (colorChanged)
                        {
                            Console.ResetColor();
                        }
                    }
                    Console.WriteLine();
                    #endregion

                    Console.WriteLine(new string('=', 36));
                }
            }
            static void ExecuteMenu()
            {
                switch (OptionCheck())
                {
                    case '1':
                        Game.Start(mode: '1'); // SinglePlayer
                        break;
                    case '2':
                        Game.Start(mode: '2'); // MultiPlayer
                        break;
                    case '3':
                        PlayerSymbols();
                        break;
                    case '4':
                        PlayerColor();
                        break;
                    case '5':
                        ToggleMusic();
                        break;
                    case '6':
                        Reset();
                        break;
                    case '7':
                        Exit();
                        break;
                }

                static char OptionCheck()
                {
                    ConsoleKeyInfo key;
                    bool success = false;

                    do
                    {
                        key = Console.ReadKey(true);

                        foreach (char option in MenuOptions)
                        {
                            if (option == key.KeyChar)
                            {
                                success = true;
                                break;
                            }
                        }
                    } while (!success);

                    Console.Clear();

                    return key.KeyChar;
                }
            }
        }

        #region Music Methods
        static void MusicHandle(bool reset = false)
        {
            if (reset && !Settings.Default.MusicActive)
            {
                music.PlayLooping();
            }
            else if (Settings.Default.MusicActive)
            {
                music.PlayLooping();
            }
            else
            {
                music.Stop();
            }
        }

        static void ToggleMusic()
        {
            if (Settings.Default.MusicActive)
            {
                Settings.Default.MusicActive = false;
            }
            else
            {
                Settings.Default.MusicActive = true;
            }

            MusicHandle();
        }
        #endregion

        #region Change Player Symbols
        static void PlayerSymbols()
        {
            if (Utility.Decision("Change Symbol For Player One Or Two?", ConsoleKey.O, ConsoleKey.T, Banner.Symbol))
            {
                SetPlayerSymbols('1');
            }
            else
            {
                SetPlayerSymbols('2');
            }

            static void SetPlayerSymbols(char player) // Player '1' Or '2'
            {
                Console.WriteLine(Banner.Symbol);

                Console.Write($"Press Player {player}'s Symbol");
                char symbol = Char.ToUpper(Console.ReadKey(true).KeyChar);

                #region Check Symbol Duplicate
                if (player == '1')
                {
                    if (symbol == Settings.Default.Player2Char || !char.IsLetter(symbol))
                    {
                        Error();
                    }
                    else
                    {
                        Settings.Default.Player1Char = symbol;
                        Settings.Default.Save();
                    }
                }
                else if (player == '2')
                {
                    if (symbol == Settings.Default.Player1Char || !char.IsLetter(symbol))
                    {
                        Error();
                    }
                    else
                    {
                        Settings.Default.Player2Char = symbol;
                        Settings.Default.Save();
                    }
                }
                #endregion

            }
        }
        #endregion

        #region Change Player Color
        static void PlayerColor()
        {
            Console.WriteLine(Banner.Color);

            if (Utility.Decision("Change Color For Player One Or Two?", ConsoleKey.O, ConsoleKey.T, Banner.Symbol))
            {
                SetPlayerColor('1');
            }
            else
            {
                SetPlayerColor('2');
            }

            static void SetPlayerColor(char player)
            {
                Console.WriteLine(Banner.Color);

                #region Output Colors
                for (int i = 0; i < PlayerColors.Count; i++)
                {
                    Console.WriteLine("{0,3}{1,-15}{2,15}{3,-2}{4}", "- ", PlayerColors[i], "| ", i, '|');
                }
                Console.WriteLine(Environment.NewLine + "Press A Key Based On The Color You Want");
                #endregion

                int color = ValidationCheck();

                #region Assign Color
                if (player == '1')
                {
                    if (color == Settings.Default.Player2Color)
                    {
                        Error();
                    }
                    else
                    {
                        Settings.Default.Player1Color = Convert.ToUInt16(color);
                        Settings.Default.Save();
                    }
                }
                else if (player == '2')
                {
                    if (color == Settings.Default.Player1Color)
                    {
                        Error();
                    }
                    else
                    {
                        Settings.Default.Player2Color = Convert.ToUInt16(color);
                        Settings.Default.Save();
                    }
                }
                #endregion

                static int ValidationCheck()
                {
                    ConsoleKeyInfo key;
                    bool success = false;

                    do
                    {
                        key = Console.ReadKey(true);

                        for (int i = 0; i < PlayerColors.Count; i++)
                        {
                            // Retrieve The Key Which Is The Color Index And Checks Whether The Key Pressed Matches One Of Them (Instead Of Doing Char Array)
                            if (PlayerColors.FirstOrDefault(x => x.Value == PlayerColors[i]).Key == Convert.ToInt32(char.GetNumericValue(key.KeyChar)))
                            {
                                success = true;
                                break;
                            }
                        }
                    } while (!success);

                    return Convert.ToInt32(char.GetNumericValue(key.KeyChar));
                }
            }
        }
        #endregion

        internal static void Error(int time = 2700)
        {
            Console.Clear();

            Console.WriteLine(Banner.Error);

            Console.WriteLine("Your Choice Is Invalid");

            Thread.Sleep(time);
        }

        static void Reset()
        {
            MusicHandle(reset: true);
            Settings.Default.Reset();
        }

        static void Exit()
        {
            Console.WriteLine(Banner.Saving);

            Settings.Default.Save();

            Thread.Sleep(1000);

            Environment.Exit(0);
        }
        #endregion
    }
}
