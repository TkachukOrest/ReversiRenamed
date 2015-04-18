using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reversi.GameEngine;
using System.IO;

namespace Reversi.GameEngine.Test
{
    [TestClass]
    public class GameTest
    {
        private TestContext m_testContext;
        public TestContext TestContext
        {
            get { return m_testContext; }
            set { m_testContext = value; }
        }

        [TestMethod]
        public void Test_CurentMove()
        {
            Game game = new Game();
            game.CurrentMove = 4;
            Assert.IsTrue(game.FirstPlayerMove() == true, "It`s time for first`s player move");

            game.CurrentMove = 5;
            Assert.IsFalse(game.FirstPlayerMove() == true, "It`s time for second`s player move");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_CurentMoveError()
        {
            Game game = new Game();
            game.CurrentMove = 0;
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_CurentMoveErrorWithNegativeArgument()
        {
            Game game = new Game();
            game.CurrentMove = -1;
        }

        [TestMethod]
        public void Test_Constructor()
        {
            Game game = new Game();
            Assert.IsTrue(game.AlreadyExitFromTimer == true, "AlreadyExitFromTimer wasn`t initialized");
            Assert.IsTrue(game.EnabledTips == true, "EnabledTips wasn`t initialized");
            Assert.IsTrue(game.EnabledComputerMoves == false, "EnabledComputerMoves wasn`t initialized");
            Assert.IsTrue(game.CurrentMove == 1, "CurrentMove wasn`t initialized");
        }

        [TestMethod]
        public void Test_InitializeField()
        {
            Game game = new Game();
            game.Initialize();
            Assert.IsTrue(game.CurrentMove == 1, "Current move ought to be 1 on start of game");
            Assert.IsTrue(game.Field.FirstPlayerPoints == 2, "First player on start have only 2 points");
            Assert.IsTrue(game.Field.SecondPlayerPoints == 2, "Second player on stat have only 2 points");
        }

        [TestMethod]
        public void Test_CreateNewGame()
        {
            Game game = new Game();
            game.CreateNewGame();
            Assert.IsTrue(game.CurrentMove == 1, "Current move ought to be 1 on start of game");
            Assert.IsTrue(game.Field.FirstPlayerPoints == 2, "First player on start have only 2 points");
            Assert.IsTrue(game.Field.SecondPlayerPoints == 2, "Second player on stat have only 2 points");
            Assert.IsTrue(game.EnabledComputerMoves == false, "Computer moves is disabled on start");
        }

        [TestMethod]
        public void Test_Move()
        {
            Game game = new Game();
            game.CreateNewGame();
            game.Field.FindEnabledMoves((int)Players.SecondPlayer);
            game.Move(2, 4);
            Assert.IsTrue(game.Field[2, 4] == -1, "Move wasn`t done");
        }


        //чи правильно віддає стейт і чи правильний рестор
        [TestMethod]
        public void Test_XMLRead()
        {
            Game game = new Game();
            game.CreateNewGame();
            string path = Path.GetDirectoryName(Path.GetDirectoryName(TestContext.TestDir)) + @"\Reversi\bin\Debug\SavedGame.xml";
            string spath = Path.GetDirectoryName(Path.GetDirectoryName(TestContext.TestDir)) + @"Reversi\bin\Debug\ErrorGame.xml";
            try
            {
                game.LoadFromXML(path);
            }
            catch (Exception ex) { Assert.IsTrue(false, "Can`t load from good path"); }
            try
            {
                game.LoadFromXML(spath);
                Assert.IsTrue(false, "Loaded from bad path");
            }
            catch (Exception ex) { }
            Assert.IsTrue(true, "Something wen`t wrong");
        }

        [TestMethod]
        public void Test_XMLWrite()
        {
            Game game = new Game();
            game.CreateNewGame();
            string path = Path.GetDirectoryName(Path.GetDirectoryName(TestContext.TestDir)) + @"\Reversi\bin\Debug\TestGame.xml";
            try
            {
                game.WriteToXML(path);
                game.LoadFromXML(path);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, "Can`t write to xml");
            }
        }
    }
}
