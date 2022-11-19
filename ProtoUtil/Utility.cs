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
            Console.Write(Banner(banner) + msg + $" ( {option1} | {option2} )");

            do
            {
                ckey = Console.ReadKey(true);
            } while (ckey.Key != option1 && ckey.Key != option2);

            Console.Clear();

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
