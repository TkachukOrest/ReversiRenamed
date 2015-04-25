using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reversi.GameEngine;

namespace Reversi.GameEngine.Test
{
    [TestClass]
    public class MatrixInitializeClass
    {
        [TestMethod]
        public void Test_FieldInitialize()
        {
            Field field = new Field();
            Assert.IsTrue(field[3, 3] == -1, "matrix[3,3] bad initialize");
            Assert.IsTrue(field[3, 4] == 1, "matrix[3,4] bad initialize");
            Assert.IsTrue(field[4, 3] == 1, "matrix[4,3] bad initialize");
            Assert.IsTrue(field[4, 4] == -1, "matrix[4,4] bad initialize");
        }
        [TestMethod]
        public void Test_CalculatePlayersPoints()
        {
            Field field = new Field();
            field.CalculatePlayersPoints();
            Assert.IsTrue(field.FirstPlayerPoints == 2, "First`s player points weren`t counted rightly");
            Assert.IsTrue(field.SecondPlayerPoints == 2, "Second`s player points weren`t counted rightly");
        }

        [TestMethod]
        public void Test_FieldCopy_WithOutRef()
        {
            Field field = new Field();
            Field field2 = new Field(field);
            //change field won`t change field2
            field[1, 1] = 1;
            Assert.IsTrue(field2[1, 1] != 1, "Matrix was copied by ref");
        }
        [TestMethod]
        public void Test_CopyMatr()
        {
            Field field = new Field();
            int[,] matr = new int[Field.N, Field.N];
            field.CopyMatr(matr);

            bool result = true;
            for (int i = 0; i < matr.GetLength(0); i++)
                for (int j = 0; j < matr.GetLength(0); j++)
                {
                    if (matr[i, j] != field[i, j])
                    {
                        result = false;
                    }
                }

            Assert.IsTrue(result, "Matrix wasn`t copied well");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_FieldIndexationPositive()
        {
            Field field = new Field();
            field[0, 4] = 5;
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_FieldIndexationNegative()
        {
            Field field = new Field();
            field[0, 4] = -5;
        }

        [TestMethod]
        public void Test_IsMovePoint()
        {
            Field field = new Field();
            field.FindEnabledMoves((int)Players.SecondPlayer);
            Assert.IsTrue(field.MovePoints.Count == 4, "We have more than 4 points");
            Assert.IsTrue(field.IsMovePoint(2, 4, (int)Players.SecondPlayer, false), "[2,4] bad move");
            Assert.IsTrue(field.IsMovePoint(3, 5, (int)Players.SecondPlayer, false), "[3,5] bad move");
            Assert.IsTrue(field.IsMovePoint(4, 2, (int)Players.SecondPlayer, false), "[4,2] bad move");
            Assert.IsTrue(field.IsMovePoint(5, 3, (int)Players.SecondPlayer, false), "[5,3] bad move");
            Assert.IsFalse(field.IsMovePoint(1, 1, (int)Players.SecondPlayer, false), "[1,1] bad move");
        }
        [TestMethod]
        public void Test_FindEnabledMoves()
        {
            Field field = new Field();
            field.FirstMoveAI = false;
            //Do player move
            field[2, 3] = (int)Players.FirstPlayer;
            field[2, 4] = (int)Players.SecondPlayer;

            field[3, 2] = (int)Players.SecondPlayer;
            field[3, 3] = (int)Players.SecondPlayer;
            field[3, 4] = (int)Players.SecondPlayer;

            field[4, 3] = (int)Players.FirstPlayer;
            field[4, 4] = (int)Players.SecondPlayer;

            field.FindEnabledMoves((int)Players.FirstPlayer);
            Assert.IsTrue(field.MovePoints.Count == 4, "We have more than 4 points");

            field.IsMovePoint(2, 1, (int)Players.FirstPlayer, true);
            field.FindEnabledMoves((int)Players.SecondPlayer);
            Assert.IsTrue(field.MovePoints.Count == 7, "We haven`t 7 points");
        }
        [TestMethod]
        public void Test_DoComputerMoveF()
        {
            //situation where better to take less points
            Field field = new Field();
            field.FirstMoveAI = false;
            //Do player move
            field[2, 3] = (int)Players.FirstPlayer;
            field[2, 4] = (int)Players.SecondPlayer;

            field[3, 2] = (int)Players.SecondPlayer;
            field[3, 3] = (int)Players.SecondPlayer;
            field[3, 4] = (int)Players.SecondPlayer;

            field[4, 3] = (int)Players.FirstPlayer;
            field[4, 4] = (int)Players.SecondPlayer;

            field.FindEnabledMoves((int)Players.FirstPlayer);
            field.DoComputerMove((int)Players.FirstPlayer);

            Assert.IsTrue((field[2, 1] == 1 || field[4, 1] == 1), "Computer do better move");
            Assert.IsFalse((field[2, 5] == 1 || field[4, 5] == 1), "Computer do bad move");
        }

        [TestMethod]
        public void Test_DoComputerMoveS()
        {
            //situation where we can choose better move with equal finall score
            Field field = new Field();
            field.FirstMoveAI = false;

            field[3, 3] = 0;
            field[3, 4] = 0;
            field[4, 3] = 0;
            field[4, 4] = 0;

            field[3, 4] = (int)Players.FirstPlayer;
            field[2, 4] = (int)Players.SecondPlayer;
            field[3, 5] = (int)Players.SecondPlayer;
            field[4, 4] = (int)Players.SecondPlayer;
            field[3, 3] = (int)Players.SecondPlayer;
            field[3, 2] = (int)Players.SecondPlayer;

            field.FindEnabledMoves((int)Players.FirstPlayer);
            field.DoComputerMove((int)Players.FirstPlayer);

            Assert.IsTrue((field[3, 1] == 1), "Computer do better move");
            Assert.IsFalse((field[1, 4] == 1 || field[3, 6] == 1 || field[5, 4] == 1), "Computer do bad move");
        }

        [TestMethod]
        public void Test_DoComputerMoveT()
        {
            //get best count of points
            Field field = new Field();
            field.FirstMoveAI = false;

            field[3, 3] = 0;
            field[3, 4] = 0;
            field[4, 3] = 0;
            field[4, 4] = 0;

            field[2, 3] = (int)Players.FirstPlayer;
            field[1, 3] = (int)Players.SecondPlayer;
            field[2, 4] = (int)Players.SecondPlayer;
            field[3, 3] = (int)Players.SecondPlayer;
            field[2, 2] = (int)Players.SecondPlayer;
            field[2, 1] = (int)Players.SecondPlayer;

            field.FindEnabledMoves((int)Players.FirstPlayer);
            field.DoComputerMove((int)Players.FirstPlayer);


            Assert.IsTrue((field[2, 0] == 1), "Computer do better move");
            Assert.IsFalse((field[0, 3] == 1 || field[2, 5] == 1 || field[4, 3] == 1), "Computer do bad move");
        }

        [TestMethod]
        public void Test_DoComputerMoveRandome()
        {
            //situation where better to take less points
            Field field = new Field();
            field.FirstMoveAI = true;
            //Do player move
            field[2, 3] = (int)Players.FirstPlayer;
            field[2, 4] = (int)Players.SecondPlayer;

            field[3, 2] = (int)Players.SecondPlayer;
            field[3, 3] = (int)Players.SecondPlayer;
            field[3, 4] = (int)Players.SecondPlayer;

            field[4, 3] = (int)Players.FirstPlayer;
            field[4, 4] = (int)Players.SecondPlayer;

            field.FindEnabledMoves((int)Players.FirstPlayer);
            field.DoComputerMove((int)Players.FirstPlayer);

            Assert.IsTrue(field.FirstMoveAI == false, "field.FirstMoveAI==false");
        }

        [TestMethod]
        public void Test_Diagonals_Check_UpLeft()
        {
            Field field = new Field();
            field.FirstMoveAI = false;
            //Do player move

            field[3, 3] = 0;
            field[3, 4] = 0;
            field[4, 3] = 0;
            field[4, 4] = 0;

            field[3, 3] = (int)Players.FirstPlayer;

            field[2, 2] = (int)Players.SecondPlayer;
            field[2, 4] = (int)Players.SecondPlayer;
            field[4, 4] = (int)Players.SecondPlayer;
            field[4, 2] = (int)Players.SecondPlayer;

            field.FindEnabledMoves((int)Players.FirstPlayer);
            Assert.IsTrue(field.MovePoints.Count == 4, "We have more than 4 points");

            field.IsMovePoint(1, 1, 1);       
            Assert.IsTrue(field[1, 1] == 1);
            Assert.IsTrue(field[2, 2] == 1);
            Assert.IsTrue(field[3, 3] == 1);
        }
        [TestMethod]
        public void Test_Diagonals_Check_UpRight()
        {
            Field field = new Field();
            field.FirstMoveAI = false;
            //Do player move

            field[3, 3] = 0;
            field[3, 4] = 0;
            field[4, 3] = 0;
            field[4, 4] = 0;

            field[3, 3] = (int)Players.FirstPlayer;

            field[2, 2] = (int)Players.SecondPlayer;
            field[2, 4] = (int)Players.SecondPlayer;
            field[4, 4] = (int)Players.SecondPlayer;
            field[4, 2] = (int)Players.SecondPlayer;

            field.FindEnabledMoves((int)Players.FirstPlayer);
            Assert.IsTrue(field.MovePoints.Count == 4, "We have more than 4 points");

            field.IsMovePoint(1, 5, 1);     
            Assert.IsTrue(field[1, 5] == 1);
            Assert.IsTrue(field[2, 4] == 1);
            Assert.IsTrue(field[3, 3] == 1);
        }
        [TestMethod]
        public void Test_Diagonals_Check_DownLeft()
        {
            Field field = new Field();
            field.FirstMoveAI = false;
            //Do player move

            field[3, 3] = 0;
            field[3, 4] = 0;
            field[4, 3] = 0;
            field[4, 4] = 0;

            field[3, 3] = (int)Players.FirstPlayer;            

            field[2, 2] = (int)Players.SecondPlayer;
            field[2, 4] = (int)Players.SecondPlayer;
            field[4, 4] = (int)Players.SecondPlayer;
            field[4, 2] = (int)Players.SecondPlayer;

            field.FindEnabledMoves((int)Players.FirstPlayer);
            Assert.IsTrue(field.MovePoints.Count == 4, "We have more than 4 points");

            field.IsMovePoint(5, 1, 1);
            Assert.IsTrue(field[5, 1] == 1);
            Assert.IsTrue(field[4, 2] == 1);
            Assert.IsTrue(field[3, 3] == 1);
        }
        [TestMethod]
        public void Test_Diagonals_Check_DownRight()
        {
            Field field = new Field();
            field.FirstMoveAI = false;
            //Do player move

            field[3, 3] = 0;
            field[3, 4] = 0;
            field[4, 3] = 0;
            field[4, 4] = 0;

            field[3, 3] = (int)Players.FirstPlayer;

            field[2, 2] = (int)Players.SecondPlayer;
            field[2, 4] = (int)Players.SecondPlayer;
            field[4, 4] = (int)Players.SecondPlayer;
            field[4, 2] = (int)Players.SecondPlayer;

            field.FindEnabledMoves((int)Players.FirstPlayer);
            Assert.IsTrue(field.MovePoints.Count == 4, "We have more than 4 points");

            field.IsMovePoint(5, 5, 1);
            Assert.IsTrue(field[5, 5] == 1);
            Assert.IsTrue(field[4, 4] == 1);
            Assert.IsTrue(field[3, 3] == 1);
        }
    }
}
