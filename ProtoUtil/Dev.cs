namespace Util
{
    public class Dev
    {
        #region Fields And Properties
        static char[,] Field { get; set; } = new char[3, 3];
        public static int XPos { get; set; } // For Guide Random Position
        public static int YPos { get; set; } // For Guide Random Position
        #endregion
        public static void PreviewRenderField(int guide)
        {
            FillField();
            ShowField(guide);
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
        static void ShowField(int guide)
        {
            for (int y = 0; y < Field.GetLength(0); y++)
            {
                if (guide == 2 && y == 0)
                {
                    for (int i = 1; i <= 3; i++)
                    {
                        if (i == 1)
                        {
                            Console.Write($"   {i}");
                        }
                        else
                        {
                            Console.Write($" {i}");
                        }
                    }
                    Console.WriteLine();
                }
                if (guide == 1)
                {
                    Console.Write($"{y + 1} |");
                }
                else
                {
                    Console.Write("  |");
                }
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

                    if (guide == 3)
                    {
                        if (x == 0 && y == 0)
                        {
                            Random rnd = new();
                            XPos = rnd.Next(0, 3);
                            YPos = rnd.Next(0, 3);
                        }
                        if (y == YPos && x == XPos)
                        {
                            Console.Write(Field[y, x]);
                        }
                        else
                        {
                            Console.Write(' ');
                        }
                    }
                    else
                    {
                        Console.Write(Field[y, x]);
                    }

                    Console.ResetColor();
                    Console.Write('|');
                }
                Console.WriteLine();
            }
        }
    }
}