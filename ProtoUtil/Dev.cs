namespace Util
{
    public class Dev
    {
        #region Fields And Properties
        static char[,] Field { get; set; } = new char[3, 3];
        #endregion
        public static void PreviewRenderField()
        {
            FillField();
            ShowField();
        }
        static void FillField()
        {
            Field[0, 0] = 'X';
            for (int y = 0; y < Field.GetLength(0); y++)
            {
                for (int x = 0; x < Field.GetLength(1); x++)
                {
                    if (x > 0)
                    {
                        if (Field[y, x - 1] == 'X')
                        {
                            Field[y, x] = 'O';
                        }
                        else if (Field[y, x - 1] == 'O') // Being Specific For Easy Bug Detection
                        {
                            Field[y, x] = 'X';
                        }
                    }
                    else if (x == 0 && y > 0)
                    {
                        if (Field[y - 1, 2] == 'X')
                        {
                            Field[y, x] = 'O';
                        }
                        else if (Field[y - 1, 2] == 'O')
                        {
                            Field[y, x] = 'X';
                        }
                    }
                }
            }
        }
        static void ShowField()
        {
            for (int y = 0; y < Field.GetLength(0); y++)
            {
                Console.Write('|');
                for (int x = 0; x < Field.GetLength(1); x++)
                {
                    if (Field[y, x] == 'X')
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    else if (Field[y, x] == 'O')
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
    }
}