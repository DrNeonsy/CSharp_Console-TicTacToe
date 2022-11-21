namespace TicTacToe
{
    internal class Players
    {
        internal char Symbole { get; set; }
        internal ConsoleColor Color { get; set; }
        internal string Name { get; set; }
        internal Players(int player, string name)
        {
            Name = name;

            switch (player)
            {
                case 1:
                    Symbole = Settings.Default.Player1Char;
                    Color = App.PlayerColors[Settings.Default.Player1Color];
                    break;
                case 2:
                    Symbole = Settings.Default.Player2Char;
                    Color = App.PlayerColors[Settings.Default.Player2Color];
                    break;
            }
        }
    }
}