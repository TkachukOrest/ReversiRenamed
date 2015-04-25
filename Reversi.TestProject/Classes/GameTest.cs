using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reversi.GameEngine;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Threading;
using System.Timers;
using Reversi.Handlers;
using Timer = System.Threading.Timer;

namespace Reversi.GameEngine.Test
{
    [TestClass]
    public class GameTest
    {
        [TestMethod]
        public void Test_CurentMove()
        {
            Game game = new Game();
            game.CurrentMove = 4;
            Assert.IsTrue(game.IsFirstPlayerMove() == true, "It`s time for first`s player move");

            game.CurrentMove = 5;
            Assert.IsFalse(game.IsFirstPlayerMove() == true, "It`s time for second`s player move");
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
            game.CreateNewGame();
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
            game.MoveTo(2, 4);
            Assert.IsTrue(game.Field[2, 4] == -1, "Move wasn`t done");
        }
        [TestMethod]
        public void Test_MoveBad()
        {
            Game game = new Game();
            game.CreateNewGame();
            game.Field.FindEnabledMoves((int)Players.SecondPlayer);
            game.MoveTo(1, 1);
            Assert.IsFalse(game.Field[1, 1] == -1, "Move was done");
        }
        [TestMethod]
        public void Test_MoveWhenGameIsEnded()
        {
            Game game = new Game();
            game.CreateNewGame();
            game.Field[3, 3] = 1;
            game.Field[3, 4] = 1;
            game.Field[4, 3] = 1;
            game.Field[4, 4] = 1;
            game.Field.FindEnabledMoves((int)Players.SecondPlayer);
            game.MoveTo(2, 3);
            Assert.IsFalse(game.Field[2, 3] == -1, "Move was done");
        }

        [TestMethod]
        public void Test_ChangeComputerModeOn()
        {
            Game game = new Game();
            game.EnableComputerMode(true);
            Assert.IsTrue(game.EnabledComputerMoves == true);
        }
        [TestMethod]
        public void Test_ChangeComputerModeOff()
        {
            Game game = new Game();
            game.EnableComputerMode(false);
            Assert.IsTrue(game.EnabledComputerMoves == false);
        }

        [TestMethod]
        public void Test_ChangeTipsOn()
        {
            Game game = new Game();
            game.EnableTips(true);
            Assert.IsTrue(game.EnabledTips == true);
        }
        [TestMethod]
        public void Test_ChangeTipsOff()
        {
            Game game = new Game();
            game.EnableTips(false);
            Assert.IsTrue(game.EnabledTips == false);
        }
        [TestMethod]
        public void Test_MoveComputer()
        {
            Game game = new Game();
            game.CreateNewGame();
            game.EnableComputerMode(true);
            game.Field.FirstMoveAI = false;
            game.Field[3, 3] = 0;
            game.Field[3, 4] = 0;
            game.Field[4, 3] = 0;
            game.Field[4, 4] = 0;

            game.Field[4, 3] = (int)Players.FirstPlayer;
            game.Field[5, 3] = (int)Players.SecondPlayer;
            game.Field[6, 3] = (int)Players.FirstPlayer;

            game.Field.FindEnabledMoves((int)Players.SecondPlayer);

            Assert.IsTrue(game.Field.MovePoints.ContainsKey(new Point(3, 3)), "Move Point doesn`t contains [3,3]");
            Assert.IsTrue(game.Field.MovePoints.ContainsKey(new Point(7, 3)), "Move Point doesn`t contains [7,3]");

            //player move      
            game.MoveTo(3, 3);
            
            Thread.Sleep(1500);
            Assert.IsTrue((game.Field[2, 3] == 1), "Computer didn`t move");
            Assert.IsTrue((game.Field[3, 3] == 1), "Didn`t convert");
            Assert.IsTrue((game.Field[4, 3] == 1), "Didn`t convert");
            Assert.IsTrue((game.Field[5, 3] == 1), "Didn`t convert");
            Assert.IsTrue((game.Field[6, 3] == 1), "Didn`t convert");
            Assert.IsTrue((game.AlreadyExitFromTimer == true), "Timer wasn`t closed");
            Assert.IsTrue((game.IsFirstPlayerMove() == false), "It must be second player move");
            Assert.IsTrue((game.Field.GameProcess == false), "Game procces must be setted to false");

            for (int i = 0; i < Field.N; i++)
                for (int j = 0; j < Field.N; j++)
                    if (game.Field[i, j] == 1 || game.Field[i, j] == -1)
                    {
                        if (
                            !((i == 2 && j == 3) || (i == 3 && j == 3) || (i == 4 && j == 3) ||
                              (i == 5 && j == 3) || (i == 6 && j == 3)))
                        {
                            Assert.IsTrue(false, String.Format("[{0};{1}]", i, j));
                        }
                    }            
        }

        [TestMethod]
        public void Test_MoveOtherPlayer()
        {
            Game game = new Game();
            game.CreateNewGame();
            game.EnableComputerMode(false);
            game.Field.FirstMoveAI = false;
            game.Field[3, 3] = 0;
            game.Field[3, 4] = 0;
            game.Field[4, 3] = 0;
            game.Field[4, 4] = 0;

            game.Field[4, 3] = (int)Players.FirstPlayer;
            game.Field[5, 3] = (int)Players.SecondPlayer;
            game.Field[6, 3] = (int)Players.FirstPlayer;

            game.Field.FindEnabledMoves((int)Players.SecondPlayer);

            Assert.IsTrue(game.Field.MovePoints.ContainsKey(new Point(3, 3)), "Move Point doesn`t contains [3,3]");
            Assert.IsTrue(game.Field.MovePoints.ContainsKey(new Point(7, 3)), "Move Point doesn`t contains [7,3]");

            game.MoveTo(3, 3);
            game.MoveTo(2, 3);

            Assert.IsTrue((game.Field[2, 3] == 1), "Computer didn`t move");
            Assert.IsTrue((game.Field[3, 3] == 1), "Didn`t convert");
            Assert.IsTrue((game.Field[4, 3] == 1), "Didn`t convert");
            Assert.IsTrue((game.Field[5, 3] == 1), "Didn`t convert");
            Assert.IsTrue((game.Field[6, 3] == 1), "Didn`t convert");
            Assert.IsTrue((game.AlreadyExitFromTimer == true), "Timer wasn`t closed");
            Assert.IsTrue((game.IsFirstPlayerMove() == false), "It must be second player move");
            Assert.IsTrue((game.Field.GameProcess == false), "Game procces must be setted to false");

            for (int i = 0; i < Field.N; i++)
                for (int j = 0; j < Field.N; j++)
                    if (game.Field[i, j] == 1 || game.Field[i, j] == -1)
                        if (!((i == 2 && j == 3) || (i == 3 && j == 3) || (i == 4 && j == 3) || (i == 5 && j == 3) || (i == 6 && j == 3)))
                            Assert.IsTrue(false, String.Format("[{0};{1}]", i, j));
        }

        [TestMethod]
        public void Test_GameFinish_FirstPlayerWin()
        {
            Game game = new Game();
            game.ShomMessageHandler += (object sender, string s) => { };
            game.CreateNewGame();
            for (int i = 0; i < Field.N; i++)
                for (int j = 0; j < Field.N; j++)
                    game.Field[i, j] = 1;
            game.Field.FindEnabledMoves((int)Players.SecondPlayer);
            game.MoveTo(1, 1);
            Assert.IsTrue(game.Field.FirstPlayerPoints == 64, "game.Field.FirstPlayerPoints == 64");
        }
        [TestMethod]
        public void Test_GameFinish_SecondPlayerWin()
        {
            Game game = new Game();
            game.ShomMessageHandler += (object sender, string s) => { };
            game.CreateNewGame();
            for (int i = 0; i < Field.N; i++)
                for (int j = 0; j < Field.N; j++)
                    game.Field[i, j] = -1;
            game.Field.FindEnabledMoves((int)Players.FirstPlayer);
            game.MoveTo(1, 1);
            Assert.IsTrue(game.Field.SecondPlayerPoints == 64, "game.Field.SecondPlayerPoints == 64");
        }
        [TestMethod]
        public void Test_GameFinish_Draw()
        {
            Game game = new Game();
            game.ShomMessageHandler += (object sender, string s) => { };
            game.CreateNewGame();
            for (int i = 0; i < Field.N; i++)
                for (int j = 0; j < Field.N; j++)
                    if (j < Field.N / 2)
                        game.Field[i, j] = -1;
                    else
                        game.Field[i, j] = 1;
            game.Field.FindEnabledMoves((int)Players.FirstPlayer);
            game.MoveTo(1, 1);
            Assert.IsTrue(game.Field.FirstPlayerPoints == 32, "game.Field.FirstPlayerPoints == 32");
            Assert.IsTrue(game.Field.SecondPlayerPoints == 32, "game.Field.SecondPlayerPoints == 32");
        }

        [TestMethod]
        public void Test_GameEvents()
        {           
            Game game = new Game();
            game.InitDrawHandler += (object sender, EventArgs args) => { }; ;
            game.UpdateScoreHandler += (object sender, EventArgs args) => { };
            game.ShomMessageHandler += (object sender, string s) => { }; ;
            game.PlayGoodSoundHandler += (object sender, EventArgs args) => { };
            game.PlayBadSoundHandler += (object sender, EventArgs args) => { };
            game.DrawHandler = (sender, args) => { };
            
            game.CreateNewGame();            
            game.Field.FindEnabledMoves((int)Players.SecondPlayer);
            //Seccond player move
            game.MoveTo(0, 0);
            Assert.IsFalse(game.Field[0, 0] == -1, "game.Field[0, 0] == -1");
            game.MoveTo(2, 4);
            Assert.IsTrue(game.Field[2, 4] == -1, "Move(second player) wasn`t done");

            //First player move
            game.MoveTo(0, 0);
            Assert.IsFalse(game.Field[0, 0] == 1, "game.Field[0, 0] == 1");
            game.MoveTo(2, 3);
            Assert.IsTrue(game.Field[2, 3] == 1, "Move(first player) wasn`t done");

        }      
    }
}
