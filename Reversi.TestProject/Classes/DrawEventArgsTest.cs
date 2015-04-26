using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reversi.GameEngine;
using Reversi.GameEngine.Classes;

namespace Reversi.TestProject.Classes
{
    [TestClass]
    public class DrawEventArgsTest
    {
        [TestMethod]
        public void Test_DrawEventArgs()
        {
            DrawEventArgs args = new DrawEventArgs((int)Players.FirstPlayer, true);
            Assert.IsTrue(args.Tips == true);
            Assert.IsTrue(args.Player == 1);
        }
    }
}
