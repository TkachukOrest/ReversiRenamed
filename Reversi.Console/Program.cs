using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reversi.GameEngine;

namespace Reversi.ConsoleProject
{
    class Program
    {
        #region Variables
        private static Drawing draw;
        private static Game game;
        #endregion

        #region Methods for Events
        private static void InitializeField()
        {         
        }
        private static void InitializeDraw()
        {
            draw = new ConsoleDrawing(game.Field);
            game.DrawHandler = draw.DrawField;
        }
        public static void UpdateScoreAndPlayerMove()
        {            
            Console.WriteLine(String.Format("Score: {0}-{1}", game.Field.FirstPlayerPoints, game.Field.SecondPlayerPoints));
        }
        public static void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }
        #endregion

        #region Main
        static void Main(string[] args)
        {
            game = new Game();
            game.InitPnlFieldHandler += InitializeField;
            game.InitDrawHandler += InitializeDraw;
            game.UpdateScoreLabelsHandler += UpdateScoreAndPlayerMove;
            game.ShomMessageHandler += ShowMessage;

            game.InitializeField();
            game.InitializeDraw();       
                        
            game.Draw((int)Players.SecondPlayer, game.EnabledTips);
            game.EnabledComputerMoves = true;

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
                game.Move(x, y);
            }
            while (key != "exit.");

            Console.ReadLine();
        }
        #endregion
    }
}
