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
            field[1, 1] = 5;
            Assert.IsTrue(field2[1, 1] != 5, "Matrix was copied by ref");
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
        }
    }
}
