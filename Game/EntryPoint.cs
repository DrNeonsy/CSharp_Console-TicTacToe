namespace TicTacToe
{
    internal class EntryPoint
    {
        static void Main(string[] args)
        {
            App.StartUp(); // Init Stuff

            while (true) App.Menu(); // Game Menu And Everything Else
        }
    }
}