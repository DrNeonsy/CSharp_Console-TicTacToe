namespace TicTacToe
{
    internal class App
    {
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
            #region Menu Point Variables
            string action1 = "SinglePlayer";
            string action2 = "MultiPlayer";
            string action3 = "Player Symbols";
            string action4 = "Toggle Music";
            string action5 = "Exit";
            #endregion

            Console.WriteLine("{0,-32}");

            Console.ReadKey();
        }
    }
}
