﻿using System;
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
	//Peer:яким чином клас користується Serializable?
    [Serializable]
    public class Field
    {
        #region Variables
        //standard size of game field
		//Peer:краще замість константи дати можливість гравцю самому обирати розмір поля
        public const int N = 8;
        public const int Scale = 40;

        //for correct AI work on first move
        public bool FirstMoveAI { get; set; }
        //0 - empty, 1- first(red) , -1 - second player(blue)        
        private int[,] _matrix;

        //List of points
        private Dictionary<Point, int> _movePoints;
        public Dictionary<Point, int> MovePoints
        {
            get
            {
                return _movePoints;
            }
        }
        #endregion

        #region Properties
        //modified only in DrawEnableMoves        
        public bool GameProcess { get; private set; }
        public int FirstPlayerPoints { get; private set; }
        public int SecondPlayerPoints { get; private set; }
        public int this[int i, int j]
        {
            get
            {
                return _matrix[i, j];
            }
            set
            {
                if (value == (int)Players.FirstPlayer | value == (int)Players.SecondPlayer | value == 0)
                {
                    _matrix[i, j] = value;
                }
                else
                {
                    throw new ArgumentException("Невірно введені дані");
                }
            }

        }
        #endregion

        #region Constructors
        public Field()
        {
            _movePoints = new Dictionary<Point, int>();
            FirstPlayerPoints = 0;
            SecondPlayerPoints = 0;
            GameProcess = true;
            FirstMoveAI = true;

            InitializeMatrix();
            CalculatePlayersPoints();
        }
        public Field(Field field)
        {
            _matrix = new int[N, N];
            Array.Copy(field._matrix, _matrix, field._matrix.Length);
            _movePoints = new Dictionary<Point, int>(field._movePoints);
        }
        #endregion

        #region Setup Methods
        private void InitializeMatrix()
        {
            _matrix = new int[N, N];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    _matrix[i, j] = 0;
                }
            }

            //blue
            _matrix[N / 2 - 1, N / 2 - 1] = -1;
            _matrix[N / 2, N / 2] = -1;

            //red
            _matrix[N / 2, N / 2 - 1] = 1;
            _matrix[N / 2 - 1, N / 2] = 1;
        }
        public void CalculatePlayersPoints()
        {
            int firstPoints = 0, secondPoints = 0; ;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (_matrix[i, j] < 0)
                    {
                        secondPoints++;
                    }
                    else
                    {
                        if (_matrix[i, j] > 0)
                        {
                            firstPoints++;
                        }
                    }
                }
            }
            FirstPlayerPoints = firstPoints;
            SecondPlayerPoints = secondPoints;
        }
        public void CopyMatr(int[,] a)
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    _matrix[i, j] = a[i, j];
                }
            }
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
            if (_matrix[x, y] == 0)
            {
                res = CheckAllDirections(x, y, player, needToAdd);
                if (res > 0 && needToAdd)
                {
                    _matrix[x, y] = player;
                }
            }
            return res;
        }

        #region SideCheck
		//забагато методів в return,  написати допоміжні методи, або порефакторити секцію Check
        private int CheckAllDirections(int x, int y, int player, bool needToAdd = true)
        {
            return UpCheck(x, y - 1, player, needToAdd) +
                    DownCheck(x, y + 1, player, needToAdd) +
                    LeftCheck(x - 1, y, player, needToAdd) +
                    RightCheck(x + 1, y, player, needToAdd) +
                    UpLeftCheck(x - 1, y - 1, player, needToAdd) +
                    UpRightCheck(x + 1, y - 1, player, needToAdd) +
                    DownRightCheck(x + 1, y + 1, player, needToAdd) +
                    DownLeftCheck(x - 1, y + 1, player, needToAdd);
        }
	//Peer:рядки 207-216 повторюються в наступних 4 методах. Можливо винести це в окремий метод? 
	//Цикли for відрізняються не суттєво в тих самих методах. Думаю можна  порефакторити або обєднати їх в один метод  
	//параметр методу needToChange краще зробити з тою самою назвою, як і в наступних є f
        private int UpCheck(int x, int y, int p, bool needToChange)
        {
            bool found = false;
            int c = 0;
            for (int i = y; i >= 0; i--)
            {
                if (_matrix[x, i] == 0) { return 0; }
                if (_matrix[x, i] == -p) { c++; }
                if (_matrix[x, i] == p)
                {
                    found = c > 0;
                    if (c > 0 && needToChange)
                    {
                        for (int j = y; j != i; j--)
                        {
                            _matrix[x, j] = p;
                        }
                    }
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
                if (_matrix[x, i] == 0) { return 0; }
                if (_matrix[x, i] == -p) { c++; }
                if (_matrix[x, i] == p)
                {
                    found = c > 0;
                    if (c > 0 && f)
                        for (int j = y; j != i; j++)
                            _matrix[x, j] = p;
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
                if (_matrix[i, y] == 0) { return 0; }
                if (_matrix[i, y] == -p) { c++; }
                if (_matrix[i, y] == p)
                {
                    found = c > 0;
                    if (c > 0 && f)
                    {
                        for (int j = x; j != i; j--)
                        {
                            _matrix[j, y] = p;
                        }
                    }
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
                if (_matrix[i, y] == 0) { return 0; }
                if (_matrix[i, y] == -p) { c++; }
                if (_matrix[i, y] == p)
                {
                    found = c > 0;
                    if (c > 0 && f)
                    {
                        for (int j = x; j != i; j++)
                        {
                            _matrix[j, y] = p;
                        }
                    }
                    break;
                }
            }
            return found ? c : 0;
        }
        #endregion SideCheck

        #region Diagonal
		//Peer: Аналогічно для наступних 4 методів
        private int UpLeftCheck(int x, int y, int p, bool f)
        {
            bool found = false;
            int i = 0, j = 0;
            int a, b;
            int c = 0;
            for (i = x, j = y; i >= 0 && j >= 0; i--, j--)
            {
                if (_matrix[i, j] == 0) { return 0; }
                if (_matrix[i, j] == -p) { c++; }
                if (_matrix[i, j] == p)
                {
                    found = c > 0;
                    if (c > 0 && f)
                    {
                        for (a = x, b = y; a != i && b != j; a--, b--)
                        {
                            _matrix[a, b] = p;
                        }
                    }
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
                if (_matrix[i, j] == 0) { return 0; }
                if (_matrix[i, j] == -p) { c++; }
                if (_matrix[i, j] == p)
                {
                    found = c > 0;
                    if (c > 0 && f)
                    {
                        for (a = x, b = y; a != i && b != j; a++, b--)
                        {
                            _matrix[a, b] = p;
                        }
                    }
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
                if (_matrix[i, j] == 0) { return 0; }
                if (_matrix[i, j] == -p) { c++; }
                if (_matrix[i, j] == p)
                {
                    found = c > 0;
                    if (c > 0 && f)
                    {
                        for (a = x, b = y; a != i && b != j; a++, b++)
                        {
                            _matrix[a, b] = p;
                        }
                    }
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
                if (_matrix[i, j] == 0) { return 0; }
                if (_matrix[i, j] == -p) { c++; }
                if (_matrix[i, j] == p)
                {
                    found = c > 0;
                    if (c > 0 && f)
                    {
                        for (a = x, b = y; a != i && b != j; a--, b++)
                        {
                            _matrix[a, b] = p;
                        }
                    }
                    break;
                }
            }
            return found ? c : 0;
        }
        #endregion Diagonal
        #endregion
    }
}
