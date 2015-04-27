using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reversi.GameEngine;
using Reversi.Handlers;

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
            IArtificialIntelligence _computerIntelligence = new MinMaxAI();
            Game game = new Game(_computerIntelligence);
            game.CreateNewGame();
            string path = Path.GetDirectoryName(Path.GetDirectoryName(TestContext.TestDir)) + @"\Reversi.TestProject\Resources\SavedGame.xml";
            try
            {
                XmlSerializer serializer = new XmlSerializer();
                GameState state = serializer.Deserialize(path);
                game.RestoreState(state);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, "Can`t load from good path");
            }
        }
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Test_XMLReadBadPath()
        {
            //в файлі є помилка, має викинутись експепшин
            string spath = Path.GetDirectoryName(Path.GetDirectoryName(TestContext.TestDir)) + @"\Reversi.TestProject\Resources\ErrorGame.xml";
            XmlSerializer serializer = new XmlSerializer();
            GameState state = serializer.Deserialize(spath);            
        }

        [TestMethod]
        public void Test_XMLWrite()
        {
            IArtificialIntelligence _computerIntelligence = new MinMaxAI();
            Game game = new Game(_computerIntelligence);
            game.CreateNewGame();
            string path = Path.GetDirectoryName(Path.GetDirectoryName(TestContext.TestDir)) + @"\Reversi.TestProject\Resources\TestGame.xml";
            try
            {
                XmlSerializer serializer = new XmlSerializer();
                serializer.Serialize(game.GetState(), path);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, "Can`t write or load from xml");
            }
        }
    }
}
