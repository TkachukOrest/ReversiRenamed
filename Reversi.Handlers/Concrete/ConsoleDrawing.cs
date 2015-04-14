using System;
using Reversi.GameEngine;

namespace Reversi.Handlers
{
    public sealed class ConsoleDrawing : Drawing
    {
        #region Constructors
        public ConsoleDrawing(Field gameField)
        {
            GameField = gameField;
        }
        #endregion

        #region Overrided abstracts methods     

        protected override void Draw(int player, bool enabledTips)
        {
            Console.Clear();
            Console.WriteLine("  1 2 3 4 5 6 7 8");            
        }

        protected override void DrawEnableMoves(int player, bool enabledTips)
        {
            GameField.FindEnabledMoves(player);

            for (int i = 0; i < Field.N; i++)
            {
                Console.Write(String.Format("{0} ",i+1));
                for (int j = 0; j < Field.N; j++)
                {
                    if (GameField[i, j] == 1)
                    {
                        Console.Write("X ");
                    }
                    if(GameField[i,j]==-1)
                    {
                        Console.Write("O ");
                    }
                    if (GameField.MovePoints.ContainsKey(new Point(i, j)))
                        Console.Write("! ");
                    else
                        if (GameField[i, j] == 0)
                        {
                            Console.Write("- ");
                        }
                }
                Console.WriteLine();
            }
        }
        #endregion
    }
}
