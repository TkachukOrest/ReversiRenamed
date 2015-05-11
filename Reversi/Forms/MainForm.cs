using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Reversi.GameEngine;
using Reversi.Handlers;

namespace Reversi
{
    public partial class MainForm : Form
    {      
        #region Variables
        private Drawing _draw;
        private Game _game;
        private GameSounds _music;
        private IArtificialIntelligence _computerIntelligence;
        #endregion

        #region Construcors
        public MainForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            _computerIntelligence = new MinMaxAI();
            _music = new GameSounds();
            
            _game = new Game(_computerIntelligence);
            SubscribeEvents();
            _game.CreateNewGame();            
        }
        #endregion

        #region Form events
        private void pnlField_Paint(object sender, PaintEventArgs e)
        {
            _game.ReDraw();
        }
        private void pnlField_MouseClick(object sender, MouseEventArgs e)
        {
            _game.MoveTo(e.Y / Field.Scale, e.X / Field.Scale);            
        }
        private void btnNewGameComputer_Click(object sender, EventArgs e)
        {
            pnlField.Enabled = true;
            _game.CreateNewGame();
            _game.EnableComputerMode(true);
        }
        private void btnNewGame_Click(object sender, EventArgs e)
        {
            pnlField.Enabled = true;
            _game.CreateNewGame();
        }     
        #endregion

        #region Methods for events
        private void SubscribeEvents()
        {
            _game.InitDrawHandler += InitializeDraw;
            _game.UpdateScoreHandler += UpdateScoreAndPlayerMove;
            _game.ShomMessageHandler += ShowMessage;
            _game.PlayGoodSoundHandler += _music.PlayGoodSound;
            _game.PlayBadSoundHandler += _music.PlayBadSound;
        }

        private void ShowMessage(object sender, string message)
        {
            MessageBox.Show(this, message, "We have a winner", MessageBoxButtons.OK);                                   
        }

        private void InitializeDraw(object sender, EventArgs e)
        {
            _draw = new FormDrawing(pnlField, _game.Field);
            _game.DrawHandler = _draw.DrawField;
        }

        private void UpdateScoreAndPlayerMove(object sender, EventArgs e)
        {
            lblFirstPlayerScore.Text = ": " + _game.Field.FirstPlayerPoints.ToString();
            lblSecondPlayerScore.Text = ": " + _game.Field.SecondPlayerPoints.ToString();            
        }
        #endregion

        #region MainMenu
        private void menu_NewGame_Click(object sender, EventArgs e)
        {
            btnNewGame.PerformClick();
        }
        private void newComputerGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnNewGameComputer.PerformClick();
        }
        private void tipsOnOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _game.EnableTips(!_game.EnabledTips);
            _game.ReDraw();
        }
        private void saveGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                saveDialog.InitialDirectory = Path.GetDirectoryName(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)) + @"\Reversi\Resources";
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    XmlSerializer serializer = new XmlSerializer();
                    serializer.Serialize(_game.GetState(), saveDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can`t save the game", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void loadLastGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                openDialog.InitialDirectory = Path.GetDirectoryName(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)) + @"\Reversi\Resources";
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    XmlSerializer serializer=new XmlSerializer();
                    GameState state = serializer.Deserialize(openDialog.FileName);
                    _game.RestoreState(state);                                       
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can`t load saved game.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm about = new AboutForm();
            about.ShowDialog();
        }
        #endregion            
    }
}
