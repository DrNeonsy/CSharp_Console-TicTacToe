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
        static char[] MenuOptions { get; } = { '1', '2', '3', '4', '5', '6' };
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
                    string action3 = "Player Symbols";
                    string action4 = "Toggle Music";
                    string action5 = "Reset Stats";
                    string action6 = "Exit";
                    #endregion

                    Console.WriteLine(Banner.Menu);

                    Console.WriteLine(new string('=', 36));
                    Console.WriteLine("{0,3}{1,-15}{2,15}{3,-2}{4}", "- ", action1, "| ", MenuOptions[0], '|');
                    Console.WriteLine("{0,3}{1,-15}{2,15}{3,-2}{4}", "- ", action2, "| ", MenuOptions[1], '|');
                    Console.WriteLine("{0,3}{1,-15}{2,15}{3,-2}{4}", "- ", action3, "| ", MenuOptions[2], '|');
                    Console.WriteLine("{0,3}{1,-15}{2,15}{3,-2}{4}", "- ", action4, "| ", MenuOptions[3], '|');
                    Console.WriteLine("{0,3}{1,-15}{2,15}{3,-2}{4}", "- ", action5, "| ", MenuOptions[4], '|');
                    Console.WriteLine("{0,3}{1,-15}{2,15}{3,-2}{4}", "- ", action6, "| ", MenuOptions[5], '|');
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

                    Console.WriteLine(Banner.Stats);

                    Console.WriteLine(new string('=', 36));
                    Console.WriteLine("{0,3}{1,-23}{2,5}{3,-4}{4}", "- ", stat1, "| ", musicState, '|');
                    Console.WriteLine("{0,3}{1,-23}{2,5}{3,2}{4,3}", "- ", stat2, "| ", Settings.Default.GamesPlayed, '|');
                    Console.WriteLine("{0,3}{1,-23}{2,5}{3,2}{4,3}", "- ", stat3, "| ", Settings.Default.Player1Char, '|');
                    Console.WriteLine("{0,3}{1,-23}{2,5}{3,2}{4,3}", "- ", stat4, "| ", Settings.Default.Player2Char, '|');
                    Console.WriteLine(new string('=', 36));
                }
            }
            static void ExecuteMenu()
            {
                switch (OptionCheck())
                {
                    case '1':
                        Game.Play(mode: 1); // SinglePlayer
                        break;
                    case '2':
                        Game.Play(mode: 2); // MultiPlayer
                        break;
                    case '3':
                        PlayerSymbols();
                        break;
                    case '4':
                        ToggleMusic();
                        break;
                    case '5':
                        Reset();
                        break;
                    case '6':
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
            if (Utility.Decision("Change Symbol Of Player One Or Two?", ConsoleKey.O, ConsoleKey.T, Banner.Symbol))
            {
                ChangePlayerSymbols('1');
            }
            else
            {
                ChangePlayerSymbols('2');
            }

            static void ChangePlayerSymbols(char player) // Player '1' Or '2'
            {
                Console.WriteLine(Banner.Symbol);

                Console.Write($"Press Player {player}'s Symbol");
                char symbol = Char.ToUpper(Console.ReadKey(true).KeyChar);

                #region Check Symbol Duplicate
                if (player == '1')
                {
                    if (symbol == Settings.Default.Player2Char)
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
                    if (symbol == Settings.Default.Player1Char)
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

                static void Error()
                {
                    Console.Clear();

                    Console.WriteLine(Banner.Error);

                    Console.WriteLine("You Cannot Use The Same Symbol For Both Players");

                    Thread.Sleep(2700);
                }
            }
        }
        #endregion

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
