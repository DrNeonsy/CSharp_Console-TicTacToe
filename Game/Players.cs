namespace TicTacToe
{
    internal class Players
    {
        internal char Symbole { get; set; }
        internal string Name { get; set; }
        internal Players(char player, string name)
        {
            Name = name;

            switch (player)
            {
                case '1':
                    Symbole = Settings.Default.Player1Char;
                    break;
                case '2':
                    Symbole = Settings.Default.Player2Char;
                    break;
            }
        }
    }
}