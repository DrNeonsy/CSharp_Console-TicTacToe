using System.Media;

namespace TicTacToe
{
    internal class Music
    {
        #region Music Object
        static SoundPlayer music = new() // Creating An Object And Setting Sound Path
        {
            Stream = Banner.BG_Music
        };
        #endregion
        internal static void MusicHandle()
        {

            if (Settings.Default.MusicActive)
            {
                music.PlayLooping();
            }
            else
            {
                music.Stop();
            }
        }
        internal static void ToggleMusic()
        {
            if (Settings.Default.MusicActive)
            {
                Settings.Default.MusicActive = false;
            }
            else
            {
                Settings.Default.MusicActive = true;
            }

            MusicHandle();
        }
    }
}
