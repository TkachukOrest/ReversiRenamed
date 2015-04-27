using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reversi.GameEngine;

namespace Reversi.TestProject.Classes
{
    [TestClass]
    public class MinMaxAITest
    {
        private IArtificialIntelligence _computerIntelligence = new MinMaxAI();

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
           _computerIntelligence.DoComputerMove(field,(int)Players.FirstPlayer);

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
           _computerIntelligence.DoComputerMove(field,(int)Players.FirstPlayer);

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
           _computerIntelligence.DoComputerMove(field,(int)Players.FirstPlayer);


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
           _computerIntelligence.DoComputerMove(field,(int)Players.FirstPlayer);

           Assert.IsTrue(field.FirstMoveAI == false, "field.FirstMoveAI==false");
       }
       
    }
}
