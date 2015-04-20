using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reversi.GameEngine;

namespace Reversi.TestProject.Classes
{
    [TestClass]
    public class XmlSerializatorTest
    {
        private TestContext m_testContext;
        public TestContext TestContext
        {
            get { return m_testContext; }
            set { m_testContext = value; }
        }

        [TestMethod]
        public void Test_XmlReadGoodPath()
        {
            Game game = new Game();
            game.CreateNewGame();
            string path = Path.GetDirectoryName(Path.GetDirectoryName(TestContext.TestDir)) + @"\Reversi.TestProject\Resources\SavedGame.xml";
            try
            {
                GameState state = new GameState();
                if (XmlSerializator.ReadFromXML(ref state, path))
                {
                    game.RestoreState(state);                    
                }
                else
                {
                    throw new Exception("Виникла помилка при зчитуванні з файлу");
                }
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, "Can`t load from good path");
            }
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Test_XMLReadBadPath()
        {
            //в файлі є помилка, має викинутись експепшин
            string spath = Path.GetDirectoryName(Path.GetDirectoryName(TestContext.TestDir)) + @"\Reversi.TestProject\Resources\ErrorGame.xml";
            GameState state = new GameState();
            if (!XmlSerializator.ReadFromXML(ref state, spath))
            {
                throw new Exception("Виникла помилка при зчитуванін з файлу");
            }
        }

        [TestMethod]
        public void Test_XMLWrite()
        {
            Game game = new Game();
            game.CreateNewGame();
            string path = Path.GetDirectoryName(Path.GetDirectoryName(TestContext.TestDir)) + @"\Reversi.TestProject\Resources\TestGame.xml";
            try
            {
                if (!XmlSerializator.WriteToXML(game.GetState(), path))
                {
                    throw new Exception("Виникла помилка при записуванні у файл");
                }
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, "Can`t write or load from xml");
            }
        }
    }
}
