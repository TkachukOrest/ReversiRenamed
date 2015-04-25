using System;

namespace Reversi.GameEngine.Classes
{
    public class DrawEventArgs:EventArgs
    {
        public int Player { get; private set; }
        public bool Tips { get; private set; }
        public DrawEventArgs(int player, bool tips)
        {
            Player = player;
            Tips = tips;
        }
    }
}
