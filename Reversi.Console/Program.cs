using System;
using Reversi.GameEngine;

namespace Reversi.ConsoleUI
{
    class Program
    {
        #region Variables
        private static Drawing _draw;
        private static Game _game;
        #endregion

        #region Methods for Events   
        private static void InitializeDraw()
        {
            _draw = new ConsoleDrawing(_game.Field);
            _game.DrawHandler = _draw.DrawField;
        }
        public static void UpdateScoreAndPlayerMove()
        {            
            Console.WriteLine(String.Format("Score: {0}-{1}", _game.Field.FirstPlayerPoints, _game.Field.SecondPlayerPoints));
        }
        public static void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }
        #endregion

        #region Main
        static void Main(string[] args)
        {
            _game = new Game();         
            _game.InitDrawHandler += InitializeDraw;
            _game.UpdateScoreLabelsHandler += UpdateScoreAndPlayerMove;
            _game.ShomMessageHandler += ShowMessage;

            _game.InitializeField();
            _game.InitializeDraw();       
                        
            _game.Draw((int)Players.SecondPlayer, _game.EnabledTips);
            _game.EnabledComputerMoves = true;

            string key;
            int x = 0, y = 0;
            do
            {
                Console.Write("Move to row and column: ");
                key = Console.ReadLine();
                if (key.Length >= 2)
                {
                    if (int.TryParse(key[0].ToString(), out x) && int.TryParse(key[1].ToString(), out y))
                    {
                        x--;
                        y--;
                    }
                    else
                        continue;
                }
                _game.Move(x, y);
            }
            while (key != "exit.");

            Console.ReadLine();
        }
        #endregion
    }
}
