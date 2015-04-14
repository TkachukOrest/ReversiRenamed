using System;
using System.Collections.Generic;
using System.Linq;

public struct Point
{
    private int _x;
    private int _y;
    public int X { get { return _x; } }
    public int Y { get { return _y; } }

    public Point(int x, int y)
    {
        _x = x;
        _y = y;
    }

}

namespace Reversi.GameEngine
{
    [Serializable]
    public class Field
    {
        #region Variables
        //standard size of game field
        public const int N = 8;
        public const int Scale = 40;
        
        public bool FirstMove { get; set; }
        //0 - empty, 1- first(red) , -1 - second player(blue)        
        private int[,] matrix;

        //List of points
        private Dictionary<Point, int> movePoints;
        public Dictionary<Point, int> MovePoints
        {
            get
            {
                return movePoints;
            }
        }
        #endregion

        #region Properties
        //modified only in DrawEnableMoves        
        public bool GameProcess { get; set; }
        public int FirstPlayerPoints { get; private set; }
        public int SecondPlayerPoints { get; private set; }
        public int this[int i, int j]
        {
            get
            {
                return matrix[i, j];
            }
            set
            {
                matrix[i, j] = value;
            }

        }
        #endregion

        #region Constructors
        public Field()
        {
            movePoints = new Dictionary<Point, int>();
            FirstPlayerPoints = 0;
            SecondPlayerPoints = 0;
            GameProcess = true;
            FirstMove = true;

            InitializeMatrix();
            CalculatePlayersPoints();
        }
        public Field(Field field)
        {
            matrix = new int[N, N];
            Array.Copy(field.matrix, matrix, field.matrix.Length);
            movePoints = new Dictionary<Point, int>(field.movePoints);
        }
        #endregion

        #region Setup Methods
        private void InitializeMatrix()
        {
            matrix = new int[N, N];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    matrix[i, j] = 0;
                }
            }

            //blue
            matrix[N / 2 - 1, N / 2 - 1] = -1;
            matrix[N / 2, N / 2] = -1;

            //red
            matrix[N / 2, N / 2 - 1] = 1;
            matrix[N / 2 - 1, N / 2] = 1;
        }        
        public void CalculatePlayersPoints()
        {
            int firstPoints = 0, secondPoints = 0; ;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (matrix[i, j] < 0)
                        secondPoints++;
                    else
                        if (matrix[i, j] > 0)
                            firstPoints++;
                }
            }
            FirstPlayerPoints = firstPoints;
            SecondPlayerPoints = secondPoints;
        }
        public void CopyMatr(int[,] a)
        {
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    matrix[i, j] = a[i, j];
        }
        #endregion

        #region AI
        public void DoComputerMove(int player)
        {
            //int count;
            //Point point = RandomStep(out count);
            Point point = BestStep(player);
            if (IsMovePoint(point.X, point.Y, player, true))
            {
                this[point.X, point.Y] = player;
            }
        }
        private Point RandomStep(out int count)
        {
            int[] values = movePoints.Values.ToArray<int>();
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
            return movePoints.Keys.ElementAt(maxPos);
        }
        private Point BestStep(int player)
        {
            int[] values = movePoints.Values.ToArray<int>();
            int[] ratings = new int[values.Length];
            int i = 0, playerCount;

            foreach (Point point in movePoints.Keys.ToArray<Point>())
            {
                ratings[i] = values[i];
                Field field = new Field(this);
                //set matrix in positions after computer move
                field.IsMovePoint(point.X, point.Y, player, true);
                field.FindEnabledMoves(-1 * player);
                //do player BestMove           
                if (field.MovePoints.Count > 0)
                {
                    Point rationalPlayerMove = field.RandomStep(out playerCount);
                    ratings[i] -= playerCount;
                }
                else
                    //if we can end game - it`s best move
                    ratings[i] = 100;
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
                    //якщо максимальний рейтинг точки співпадає з даним - вибираємо ту, яка  впершому ході забере більше фішок
                    //дасть перевагу, якщо гравець допустить помилку
                    if (max == ratings[i] && willget < values[i])
                    {
                        max = ratings[i];
                        maxPos = i;
                        willget = values[i];
                    }
            }
            if (FirstMove)
            {
                Random rnd = new Random();
                FirstMove = false;
                return movePoints.Keys.ElementAt(rnd.Next(0, 3));                
            }
            else
                return movePoints.Keys.ElementAt(maxPos);
        }
        #endregion

        #region Algorithm Methods
        public bool IsMovePoint(int x, int y, int player, bool needToAdd = true)
        {
            if (MovePoints.ContainsKey(new Point(x, y)))
            {
                CanMove(x, y, player, needToAdd);
                return true;
            }
            else
            {
                return false;
            }
        }
        public void FindEnabledMoves(int player)
        {
            MovePoints.Clear();
            for (int i = 0; i < Field.N; i++)
            {
                for (int j = 0; j < Field.N; j++)
                {
                    int numberOfMoves = CanMove(i, j, player, false);
                    if (numberOfMoves > 0)
                    {
                        MovePoints.Add(new Point(i, j), numberOfMoves);
                    }
                }
            }
            GameProcess = MovePoints.Count > 0;
        }
        private int CanMove(int x, int y, int player, bool needToAdd = true)
        {
            int res = 0;
            if (matrix[x, y] == 0)
            {
                res = UpCheck(x, y - 1, player, needToAdd) +
                    DownCheck(x, y + 1, player, needToAdd) +
                    LeftCheck(x - 1, y, player, needToAdd) +
                    RightCheck(x + 1, y, player, needToAdd) +
                    UpLeftCheck(x - 1, y - 1, player, needToAdd) +
                    UpRightCheck(x + 1, y - 1, player, needToAdd) +
                    DownRightCheck(x + 1, y + 1, player, needToAdd) +
                    DownLeftCheck(x - 1, y + 1, player, needToAdd);
                if (res > 0)
                {
                    if (needToAdd)
                    {
                        matrix[x, y] = player;
                    }
                }
            }
            return res;
        }

        #region SideCheck
        //справа на ліво(до 0)
        private int UpCheck(int x, int y, int p, bool needToChange)
        {
            bool found = false;
            int c = 0;
            for (int i = y; i >= 0; i--)
            {
                //якщо точка в яку ми клікнули не прилягає до жодної фішки (має відступ)
                //використовуєтсья для правильного пошуку можливих ходів
                if (matrix[x, i] == 0) return 0;
                if (matrix[x, i] == -p) c++;
                if (matrix[x, i] == p)
                {
                    found = c > 0;
                    if (c > 0 && needToChange)
                        for (int j = y; j != i; j--)
                            matrix[x, j] = p;
                    break;
                }
            }
            return found ? c : 0;
        }
        private int DownCheck(int x, int y, int p, bool f)
        {
            bool found = false;
            int c = 0;
            for (int i = y; i < N; i++)
            {
                if (matrix[x, i] == 0) return 0;
                if (matrix[x, i] == -p) c++;
                if (matrix[x, i] == p)
                {
                    found = c > 0;
                    if (c > 0 && f)
                        for (int j = y; j != i; j++)
                            matrix[x, j] = p;
                    break;
                }
            }
            return found ? c : 0;
        }
        private int LeftCheck(int x, int y, int p, bool f)
        {
            bool found = false;
            int c = 0;
            for (int i = x; i >= 0; i--)
            {
                if (matrix[i, y] == 0) return 0;
                if (matrix[i, y] == -p) c++;
                if (matrix[i, y] == p)
                {
                    found = c > 0;
                    if (c > 0 && f)
                        for (int j = x; j != i; j--)
                            matrix[j, y] = p;
                    break;
                }
            }
            return found ? c : 0;
        }
        private int RightCheck(int x, int y, int p, bool f)
        {
            bool found = false;
            int c = 0;
            for (int i = x; i < N; i++)
            {
                if (matrix[i, y] == 0) return 0;
                if (matrix[i, y] == -p) c++;
                if (matrix[i, y] == p)
                {
                    found = c > 0;
                    if (c > 0 && f)
                        for (int j = x; j != i; j++)
                            matrix[j, y] = p;
                    break;
                }
            }
            return found ? c : 0;
        }
        #endregion SideCheck

        #region Diagonal
        private int UpLeftCheck(int x, int y, int p, bool f)
        {
            bool found = false;
            int i = 0, j = 0;
            int a, b;
            int c = 0;
            for (i = x, j = y; i >= 0 && j >= 0; i--, j--)
            {
                if (matrix[i, j] == 0) return 0;
                if (matrix[i, j] == -p) c++;
                if (matrix[i, j] == p)
                {
                    found = c > 0;
                    if (c > 0 && f)
                        for (a = x, b = y; a != i && b != j; a--, b--)
                            matrix[a, b] = p;
                    break;
                }
            }
            return found ? c : 0;
        }

        private int UpRightCheck(int x, int y, int p, bool f)
        {
            bool found = false;
            int i = 0, j = 0;
            int a, b;
            int c = 0;
            for (i = x, j = y; i < N && j >= 0; i++, j--)
            {
                if (matrix[i, j] == 0) return 0;
                if (matrix[i, j] == -p) c++;
                if (matrix[i, j] == p)
                {
                    found = c > 0;
                    if (c > 0 && f)
                        for (a = x, b = y; a != i && b != j; a++, b--)
                            matrix[a, b] = p;
                    break;
                }
            }
            return found ? c : 0;
        }


        private int DownRightCheck(int x, int y, int p, bool f)
        {
            bool found = false;
            int i = 0, j = 0;
            int a, b;
            int c = 0;
            for (i = x, j = y; i < N && j < N; i++, j++)
            {
                if (matrix[i, j] == 0) return 0;
                if (matrix[i, j] == -p) c++;
                if (matrix[i, j] == p)
                {
                    found = c > 0;
                    if (c > 0 && f)
                        for (a = x, b = y; a != i && b != j; a++, b++)
                            matrix[a, b] = p;
                    break;
                }
            }
            return found ? c : 0;
        }

        private int DownLeftCheck(int x, int y, int p, bool f)
        {
            bool found = false;
            int i = 0, j = 0;
            int a, b;
            int c = 0;
            for (i = x, j = y; i >= 0 && j < N; i--, j++)
            {
                if (matrix[i, j] == 0) return 0;
                if (matrix[i, j] == -p) c++;
                if (matrix[i, j] == p)
                {
                    found = c > 0;
                    if (c > 0 && f)
                        for (a = x, b = y; a != i && b != j; a--, b++)
                            matrix[a, b] = p;
                    break;
                }
            }
            return found ? c : 0;
        }
        #endregion Diagonal
        #endregion
    }
}
