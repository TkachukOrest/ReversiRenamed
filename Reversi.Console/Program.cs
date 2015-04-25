using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Reversi.GameEngine;
using Reversi.Handlers;


namespace Reversi.ConsoleUI
{
    class Program
    {
        #region Variables
        private static Drawing _draw;
        private static Game _game;
        private static GameSounds _music;
        #endregion

        #region Methods for Events
        private static void InitializeDraw(object sender, EventArgs e)
        {
            _draw = new ConsoleDrawing(_game.Field);
            _game.DrawHandler = _draw.DrawField;
        }
        public static void UpdateScoreAndPlayerMove(object sender, EventArgs e)
        {
            Console.WriteLine(String.Format("Score: X:{0} - O:{1}", _game.Field.FirstPlayerPoints, _game.Field.SecondPlayerPoints));
        }
        public static void ShowMessage(object sender, string message)
        {
            Console.WriteLine(message);
        }
        #endregion

        #region Main
        [STAThread]
        static void Main(string[] args)
        {
            ShowMenu();

            _music = new GameSounds();
            _game = new Game();

            _game.InitDrawHandler += InitializeDraw;
            _game.UpdateScoreHandler += UpdateScoreAndPlayerMove;
            _game.ShomMessageHandler += ShowMessage;
            _game.PlayGoodSoundHandler += _music.PlayGoodSound;
            _game.PlayBadSoundHandler += _music.PlayBadSound;

            _game.CreateNewGame();
            _game.EnableComputerMode(true);

            string key;
            char option;
            bool gameProcess = true;
            int x = 0, y = 0;
            do
            {
                key = Console.ReadLine();
                if (key.Length < 2)
                {
                    if (char.TryParse(key.ToUpper(), out option))
                    {
                        MenuProcess(option, ref gameProcess);
                    }
                }
                else
                {
                    if (int.TryParse(key[0].ToString(), out x) && int.TryParse(key[1].ToString(), out y))
                    {
                        x--;
                        y--;
                    }
                    else
                        continue;
                    _game.MoveTo(x, y);                    
                }
            }
            while (gameProcess);
            Console.ReadLine();
        }
        #endregion

        #region Helpers
        static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("Options:");
            Console.WriteLine("First player(computer in AI mode) - X, Second player - O.\n");
            Console.WriteLine("To move to the position- write \t'xy'.");
            Console.WriteLine("To end game - write \t'Q'.");
            Console.WriteLine("To start new game - write \t'N'.");
            Console.WriteLine("To start new game with computer- write \t'C'.");
            Console.WriteLine("To enable|disable tips- write \t'T'.");
            Console.WriteLine("To save game- write \t'S'.");
            Console.WriteLine("To load game- write \t'L'.");
            Console.WriteLine("To call options- write \t'O'.");
            Console.WriteLine("\nPress any key to start...");
            Console.Read();
            if (_game != null)
            {
                _game.ReDraw();                
            }
        }

        static void MenuProcess(char option, ref bool gameProcess)
        {
            switch (option)
            {
                case 'Q':
                    gameProcess = false;
                    break;
                case 'N':
                    _game.CreateNewGame();
                    _game.EnableComputerMode(false);
                    break;
                case 'C':
                    _game.CreateNewGame();
                    _game.EnableComputerMode(true);
                    break;
                case 'T':
                    _game.EnableTips(!(_game.EnabledTips));
                    _game.ReDraw();
                    break;
                case 'S':
                    SaveGame();
                    break;
                case 'L':
                    LoadGame();
                    break;
                case 'O':
                    ShowMenu();
                    break;
                default:                    
                    break;
            }
        }
        static void SaveGame()
        {
            try
            {
                SaveFileDialog  saveDialog =new SaveFileDialog();
                saveDialog.InitialDirectory = Path.GetDirectoryName(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    XmlSerializer serializer = new XmlSerializer();
                    serializer.Serialize(_game.GetState(), saveDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неможливо зберегти гру", "Помилка", MessageBoxButtons.OK);
            }
        }
        static void LoadGame()
        {
            try
            {
                OpenFileDialog openDialog=new OpenFileDialog();
                openDialog.InitialDirectory = Path.GetDirectoryName(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    XmlSerializer serializer = new XmlSerializer();
                    GameState state = serializer.Deserialize(openDialog.FileName);
                    _game.RestoreState(state);                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неможливо загрузити збережену гру.", "Помилка", MessageBoxButtons.OK);
            }
        }
        #endregion
    }
}
