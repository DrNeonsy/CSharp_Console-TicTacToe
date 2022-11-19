namespace TicTacToe
{
    internal class Game
    {
        #region Fields And Properties
        static Players[] Players { get; set; } = new Players[2];
        static char[,] Field { get; set; } = new char[3, 3];
        #endregion

        #region Methods
        internal static void Play(int mode)
        {

        }
        static void ShowField()
        {
            for (int y = 0; y < Field.GetLength(0); y++)
            {
                Console.Write('|');
                for (int x = 0; x < Field.GetLength(1); x++)
                {
                    if (Field[y, x] == Players[0].Symbole)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    else if (Field[y, x] == Players[1].Symbole)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
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
