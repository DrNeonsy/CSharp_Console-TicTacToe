namespace Util
{
    public class Utility
    {
        /// <summary>
        /// Prompts The User To Press One Of Two Keys
        /// </summary>
        /// <param name="msg">The Message You Wish To Display</param>
        /// <param name="option1">Key 1</param>
        /// <param name="option2">Key 2</param>
        /// <param name="banner">Optional String Banner</param>
        /// <returns>True If Key 1 Has Been Pressed Otherwise False</returns>
        public static bool Decision(string msg, ConsoleKey option1 = ConsoleKey.Y, ConsoleKey option2 = ConsoleKey.N, string? banner = null)
        {
            ConsoleKeyInfo ckey;
            Console.WriteLine(Banner(banner));

            do
            {
                Console.Write(msg + $" ( {option1} | {option2} )");
                ckey = Console.ReadKey(true);

                if (ckey.Key != option1 && ckey.Key != option2)
                {
                    Console.Clear();
                    Console.Write(Banner(banner));
                    Console.WriteLine($"Only Use {option1} Or {option2} Key" +
                        Environment.NewLine);
                }

            } while (ckey.Key != option1 && ckey.Key != option2);
            return ckey.Key == option1;
        }
        
        static string Banner(string? banner)
        {
            if (banner != null)
            {
               return banner + Environment.NewLine;
            }
            else
            {
                return null;
            }
        }
    }
}
