using System;

namespace TicTacToe
{
    internal class App
    {
        #region Fields And Properties
        static char[] MenuOptions { get; } = { '1', '2', '3', '4', '5' };
        #endregion

        #region Methods
        internal static void StartUp()
        {
            Config();
            Intro();

            static void Config()
            {
                Console.CursorVisible = false;
                Console.Title = "Tic Tac Toe";
            }

            static void Intro()
            {
                Console.WriteLine(Banner.Tic);
                Thread.Sleep(555);
                Console.Clear();

                Console.WriteLine(Banner.Tac);
                Thread.Sleep(555);
                Console.Clear();

                Console.WriteLine(Banner.Toe);
                Thread.Sleep(1000);
                Console.Clear();
            }
        }

        internal static void Menu()
        {
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
                    string action5 = "Exit";
                    #endregion

                    Console.WriteLine(Banner.Menu);

                    Console.WriteLine(new string('=', 39));
                    Console.WriteLine("{0,3}{1,-18}{2,15}{3,-2}{4}", "- ", action1, "| ", MenuOptions[0], '|');
                    Console.WriteLine("{0,3}{1,-18}{2,15}{3,-2}{4}", "- ", action2, "| ", MenuOptions[1], '|');
                    Console.WriteLine("{0,3}{1,-18}{2,15}{3,-2}{4}", "- ", action3, "| ", MenuOptions[2], '|');
                    Console.WriteLine("{0,3}{1,-18}{2,15}{3,-2}{4}", "- ", action4, "| ", MenuOptions[3], '|');
                    Console.WriteLine("{0,3}{1,-18}{2,15}{3,-2}{4}", "- ", action5, "| ", MenuOptions[4], '|');
                    Console.WriteLine(new string('=', 39));
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
                        musicState = "On";
                    }
                    else
                    {
                        musicState = "Off";
                    }
                    #endregion

                    Console.WriteLine(Banner.Stats);

                    Console.WriteLine(new string('=', 39));
                    Console.WriteLine("{0,3}{1,-26}{2,5}{3,-4}{4}", "- ", stat1, "| ", musicState, '|');
                    Console.WriteLine("{0,3}{1,-26}{2,5}{3,-4}{4}", "- ", stat2, "| ", Settings.Default.GamesPlayed, '|');
                    Console.WriteLine("{0,3}{1,-26}{2,5}{3,-4}{4}", "- ", stat3, "| ", Settings.Default.Player1Char, '|');
                    Console.WriteLine("{0,3}{1,-26}{2,5}{3,-4}{4}", "- ", stat4, "| ", Settings.Default.Player2Char, '|');
                    Console.WriteLine(new string('=', 39));
                }
            }
            static void ExecuteMenu()
            {
                switch (OptionCheck())
                {
                    default:
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
                    return key.KeyChar;
                }
            }
        }
        #endregion
    }
}
