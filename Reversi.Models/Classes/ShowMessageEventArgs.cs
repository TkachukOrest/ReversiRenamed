using System;

namespace Reversi.GameEngine
{
    public class ShowMessageEventArgs:EventArgs
    {
        public string Message { get; private set; }

        public ShowMessageEventArgs(string message)
        {
            Message = message;
        }
    }
}
