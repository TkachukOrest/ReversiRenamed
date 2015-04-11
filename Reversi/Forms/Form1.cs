using System;
using System.Windows.Forms;
using Reversi.GameEngine;

namespace Reversi
{
    public partial class MainForm : Form
    {
        #region Variables
        private Drawing _draw;
        private Game _game;
        #endregion

        #region Construcors
        public MainForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            _game = new Game();
            _game.InitDrawHandler += InitializeDraw;
            _game.UpdateScoreLabelsHandler += UpdateScoreAndPlayerMove;
            _game.ShomMessageHandler += ShowMessage;

            _game.InitializeField();
            _game.InitializeDraw();
        }
        #endregion

        #region Form events
        private void pnl_Field_Paint(object sender, PaintEventArgs e)
        {
            if (_game.AlreadyExitFromTimer)
            {
                if (_game.FirstPlayerMove())
                    _game.Draw((int)Players.FirstPlayer, _game.EnabledTips);
                else
                    _game.Draw((int)Players.SecondPlayer, _game.EnabledTips);
            }
        }
        private void pnl_Field_MouseClick(object sender, MouseEventArgs e)
        {
            _game.Move(e.Y / Field.Scale, e.X / Field.Scale);
        }
        private void btn_newGameComputer_Click(object sender, EventArgs e)
        {
            pnl_Field.Enabled = true;
            _game.CreateNewGame();
            _game.EnabledComputerMoves = true;
        }
        private void btn_newGame_Click(object sender, EventArgs e)
        {
            pnl_Field.Enabled = true;
            _game.CreateNewGame();
        }
        private void cb_tips_Changed(object sender, EventArgs e)
        {
            _game.EnabledTips = cb_tips.Checked;
            if (_game.FirstPlayerMove())
                _game.Draw((int)Players.FirstPlayer, _game.EnabledTips);
            else
                _game.Draw((int)Players.SecondPlayer, _game.EnabledTips);
        }
        #endregion

        #region Methods for events
        public void ShowMessage(string message)
        {
            MessageBox.Show(null, message, "We have a winner", MessageBoxButtons.OK);
        }
        private void InitializeDraw()
        {
            _draw = new FormDrawing(pnl_Field, _game.Field);
            _game.DrawHandler = _draw.DrawField;
        }
        private void UpdateScoreAndPlayerMove()
        {
            lbl_firstPlayerScore.Text = "Red player: " + _game.Field.FirstPlayerPoints.ToString();
            lbl_secondPlayerScore.Text = "Blue player: " + _game.Field.SecondPlayerPoints.ToString();
            lbl_NextMove.Text = _game.FirstPlayerMove() ? "Next move: red" : "Next move: blue";
        }
        #endregion

        #region MainMenu
        private void menu_NewGame_Click(object sender, EventArgs e)
        {
            btn_newGame.PerformClick();
        }
        private void newComputerGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btn_newGameComputer.PerformClick();
        }
        private void saveGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                if (!XmlSerializator.WriteToXML(_game.Field, _game.EnabledTips, _game.CurrentMove, saveDialog.FileName))
                {
                    MessageBox.Show("Виникла помилка при записуванні в файл.", "Помилка :(", MessageBoxButtons.OK);
                }
            }
        }
        private void loadLastGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    _game.LoadFromXML(openDialog.FileName);
                    cb_tips.Checked = _game.EnabledTips;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка :(", MessageBoxButtons.OK);
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
