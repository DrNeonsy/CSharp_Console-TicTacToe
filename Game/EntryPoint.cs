namespace TicTacToe
{
    internal class EntryPoint
    {
        static void Main(string[] args)
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


            Dev.PreviewRenderField();
            Console.ReadKey();
            Settings.Default.MusicActive= true;
            Settings.Default.Save();
        }
    }
}