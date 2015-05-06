using System;
using System.Linq;

namespace Reversi.GameEngine
{
    public class MinMaxAI:IArtificialIntelligence
    {
        public void DoComputerMove(Field field,int player)
        {
            Point point = BestStep(field,player);
            if (field.IsMovePoint(point.X, point.Y, player, true))
            {
                field[point.X, point.Y] = player;
            }
        }

        public Point RandomStep(Field field,out int count)
        {
            int[] values = field.MovePoints.Values.ToArray<int>();
            int maxPos = 0, max = values[0];
            for (int i = 0; i < values.Length; i++)
            {
                if (max < values[i])
                {
                    max = values[i];
                    maxPos = i;
                }
            }
            count = values[maxPos];
            return field.MovePoints.Keys.ElementAt(maxPos);
        }

        public Point BestStep(Field gameField,int player)
        {

            int[] values = gameField.MovePoints.Values.ToArray<int>();
            int[] ratings = new int[values.Length];
            int i = 0;

            foreach (Point point in gameField.MovePoints.Keys.ToArray<Point>())
            {
                ratings[i] = values[i];
                Field field = new Field(gameField);
                //set matrix in positions after computer move
                field.IsMovePoint(point.X, point.Y, player, true);
                field.FindEnabledMoves(-1 * player);
                //do player BestMove           
                if (field.MovePoints.Count > 0)
                {
                    var playerCount = 0;
                    Point rationalPlayerMove = this.RandomStep(field,out playerCount);
                    ratings[i] -= playerCount;
                }
                else
                {
                    //if we can end game - it`s best move
                    ratings[i] = 100;
                }
                i++;
            }

            int maxPos = 0, max = ratings[0], willget = values[0];
            for (i = 0; i < ratings.Length; i++)
            {
                if (max < ratings[i])
                {
                    max = ratings[i];
                    maxPos = i;
                    willget = values[i];
                }
                else
                {
                    //якщо максимальний рейтинг точки співпадає з даним - вибираємо ту, яка  впершому ході забере більше фішок
                    //дасть перевагу, якщо гравець допустить помилку
                    if (max == ratings[i] && willget < values[i])
                    {
                        max = ratings[i];
                        maxPos = i;
                        willget = values[i];
                    }
                }
            }
            if (gameField.FirstMoveAI)
            {
                Random rnd = new Random();
                gameField.FirstMoveAI = false;
                return gameField.MovePoints.Keys.ElementAt(rnd.Next(0, gameField.MovePoints.Count));
            }
            /*			
                Review VV:
                    додати фігурні дужки
            */
            else
                return gameField.MovePoints.Keys.ElementAt(maxPos);
        }
    }
}
