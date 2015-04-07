using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using Reversi.GameEngine;


namespace Reversi
{
    public partial class MainForm : Form
    {
        #region Variables    
        private Drawing draw;
        private Game game;
        #endregion

        #region Construcors
        public MainForm()
        {
            InitializeComponent();
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;

            game = new Game();
            game.InitPnlFieldHandler += InitializeField;
            game.InitDrawHandler += InitializeDraw;
            game.UpdateScoreLabelsHandler += UpdateScoreAndPlayerMove;
            game.ShomMessageHandler += ShowMessage;

            game.InitializeField();
            game.InitializeDraw();   
        }
        #endregion

        #region Form events
        private void pnl_Field_Paint(object sender, PaintEventArgs e)
        {            
            if (game.AlreadyExitFromTimer)
            {
                if ((game.CurrentMove) % 2 == 0)
                    game.Draw((int)Players.FirstPlayer, game.EnabledTips);
                else
                    game.Draw((int)Players.SecondPlayer, game.EnabledTips);
            }
        }
        private void pnl_Field_MouseClick(object sender, MouseEventArgs e)
        {            
            game.Move(e.Y / Field.Scale, e.X / Field.Scale);            
        }
        private void btn_newGameComputer_Click(object sender, EventArgs e)
        {
            pnl_Field.Enabled = true;
            game.CreateNewGame();
            game.EnabledComputerMoves = true;
        }
        private void btn_newGame_Click(object sender, EventArgs e)
        {
            pnl_Field.Enabled = true;
            game.CreateNewGame();
        }
        private void cb_tips_Changed(object sender, EventArgs e)
        {
            game.EnabledTips = cb_tips.Checked;
            if ((game.CurrentMove) % 2 == 0)
                game.Draw((int)Players.FirstPlayer, game.EnabledTips);
            else
                game.Draw((int)Players.SecondPlayer, game.EnabledTips);
        }
        #endregion

        #region Methods for events
        public void ShowMessage(string message)
        {
            MessageBox.Show(null, message, "We have a winner", MessageBoxButtons.OK);
        }
        private void InitializeField()
        {
            pnl_Field.Width = Field.N * Field.Scale;
            pnl_Field.Height = Field.N * Field.Scale;
        }
        private void InitializeDraw()
        {
            draw = new FormDrawing(pnl_Field, game.Field);
            game.DrawHandler = draw.DrawField;
        }
        private void UpdateScoreAndPlayerMove()
        {
            lbl_firstPlayerScore.Text = "Red player: " + game.Field.FirstPlayerPoints.ToString();
            lbl_secondPlayerScore.Text = "Blue player: " + game.Field.SecondPlayerPoints.ToString();
            if ((game.CurrentMove) % 2 == 0)
                lbl_NextMove.Text = "Next move: red";
            else
                lbl_NextMove.Text = "Next move: blue";
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
                if (!XmlSerializator.WriteToXML(game.Field, game.EnabledTips, game.CurrentMove, saveDialog.FileName))
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
                    game.LoadFromXML(openDialog.FileName);
                    cb_tips.Checked = game.EnabledTips;
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
