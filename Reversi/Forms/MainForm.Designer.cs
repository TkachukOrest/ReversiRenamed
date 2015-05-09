namespace Reversi
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pnl_Field = new System.Windows.Forms.Panel();
            this.btn_newGame = new System.Windows.Forms.Button();
            this.lbl_firstPlayerScore = new System.Windows.Forms.Label();
            this.lbl_secondPlayerScore = new System.Windows.Forms.Label();
            this.lbl_NextMove = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_NewGame = new System.Windows.Forms.ToolStripMenuItem();
            this.newComputerGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadLastGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveDialog = new System.Windows.Forms.SaveFileDialog();
            this.cb_tips = new System.Windows.Forms.CheckBox();
            this.lbl_tips = new System.Windows.Forms.Label();
            this.btn_newGameComputer = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_Field
            // 
            this.pnl_Field.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.pnl_Field.Location = new System.Drawing.Point(12, 154);
            this.pnl_Field.Name = "pnl_Field";
            this.pnl_Field.Size = new System.Drawing.Size(320, 320);
            this.pnl_Field.TabIndex = 0;
            this.pnl_Field.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_Field_Paint);
            this.pnl_Field.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnl_Field_MouseClick);
            // 
            // btn_newGame
            // 
            this.btn_newGame.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_newGame.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_newGame.Location = new System.Drawing.Point(12, 82);
            this.btn_newGame.Name = "btn_newGame";
            this.btn_newGame.Size = new System.Drawing.Size(320, 23);
            this.btn_newGame.TabIndex = 0;
            this.btn_newGame.Text = "New game with player";
            this.btn_newGame.UseVisualStyleBackColor = false;
            this.btn_newGame.Click += new System.EventHandler(this.btn_newGame_Click);
            // 
            // lbl_firstPlayerScore
            // 
            this.lbl_firstPlayerScore.AutoSize = true;
            this.lbl_firstPlayerScore.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lbl_firstPlayerScore.Location = new System.Drawing.Point(12, 38);
            this.lbl_firstPlayerScore.Name = "lbl_firstPlayerScore";
            this.lbl_firstPlayerScore.Size = new System.Drawing.Size(48, 13);
            this.lbl_firstPlayerScore.TabIndex = 1;
            this.lbl_firstPlayerScore.Text = "Player 1:";
            // 
            // lbl_secondPlayerScore
            // 
            this.lbl_secondPlayerScore.AutoSize = true;
            this.lbl_secondPlayerScore.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lbl_secondPlayerScore.Location = new System.Drawing.Point(12, 62);
            this.lbl_secondPlayerScore.Name = "lbl_secondPlayerScore";
            this.lbl_secondPlayerScore.Size = new System.Drawing.Size(48, 13);
            this.lbl_secondPlayerScore.TabIndex = 2;
            this.lbl_secondPlayerScore.Text = "Player 2:";
            // 
            // lbl_NextMove
            // 
            this.lbl_NextMove.AutoSize = true;
            this.lbl_NextMove.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lbl_NextMove.Location = new System.Drawing.Point(169, 38);
            this.lbl_NextMove.Name = "lbl_NextMove";
            this.lbl_NextMove.Size = new System.Drawing.Size(61, 13);
            this.lbl_NextMove.TabIndex = 3;
            this.lbl_NextMove.Text = "Next move:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(338, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_NewGame,
            this.newComputerGameToolStripMenuItem,
            this.saveGameToolStripMenuItem,
            this.loadLastGameToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "&Menu";
            // 
            // menu_NewGame
            // 
            this.menu_NewGame.Name = "menu_NewGame";
            this.menu_NewGame.Size = new System.Drawing.Size(186, 22);
            this.menu_NewGame.Text = "&New  game";
            this.menu_NewGame.Click += new System.EventHandler(this.menu_NewGame_Click);
            // 
            // newComputerGameToolStripMenuItem
            // 
            this.newComputerGameToolStripMenuItem.Name = "newComputerGameToolStripMenuItem";
            this.newComputerGameToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.newComputerGameToolStripMenuItem.Text = "New computer game";
            this.newComputerGameToolStripMenuItem.Click += new System.EventHandler(this.newComputerGameToolStripMenuItem_Click);
            // 
            // saveGameToolStripMenuItem
            // 
            this.saveGameToolStripMenuItem.Name = "saveGameToolStripMenuItem";
            this.saveGameToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.saveGameToolStripMenuItem.Text = "&Save game";
            this.saveGameToolStripMenuItem.Click += new System.EventHandler(this.saveGameToolStripMenuItem_Click);
            // 
            // loadLastGameToolStripMenuItem
            // 
            this.loadLastGameToolStripMenuItem.Name = "loadLastGameToolStripMenuItem";
            this.loadLastGameToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.loadLastGameToolStripMenuItem.Text = "&Load game";
            this.loadLastGameToolStripMenuItem.Click += new System.EventHandler(this.loadLastGameToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(183, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // openDialog
            // 
            this.openDialog.FileName = "openFileDialog1";
            this.openDialog.Filter = "XML|*.xml";
            // 
            // saveDialog
            // 
            this.saveDialog.Filter = "XML|*.xml";
            // 
            // cb_tips
            // 
            this.cb_tips.AutoSize = true;
            this.cb_tips.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cb_tips.Checked = true;
            this.cb_tips.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_tips.Location = new System.Drawing.Point(232, 58);
            this.cb_tips.Name = "cb_tips";
            this.cb_tips.Size = new System.Drawing.Size(15, 14);
            this.cb_tips.TabIndex = 5;
            this.cb_tips.UseVisualStyleBackColor = false;
            this.cb_tips.CheckedChanged += new System.EventHandler(this.cb_tips_Changed);
            // 
            // lbl_tips
            // 
            this.lbl_tips.AutoSize = true;
            this.lbl_tips.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lbl_tips.Location = new System.Drawing.Point(169, 59);
            this.lbl_tips.Name = "lbl_tips";
            this.lbl_tips.Size = new System.Drawing.Size(30, 13);
            this.lbl_tips.TabIndex = 6;
            this.lbl_tips.Text = "Tips:";
            // 
            // btn_newGameComputer
            // 
            this.btn_newGameComputer.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_newGameComputer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_newGameComputer.Location = new System.Drawing.Point(12, 111);
            this.btn_newGameComputer.Name = "btn_newGameComputer";
            this.btn_newGameComputer.Size = new System.Drawing.Size(320, 23);
            this.btn_newGameComputer.TabIndex = 7;
            this.btn_newGameComputer.Text = "New game with computer";
            this.btn_newGameComputer.UseVisualStyleBackColor = false;
            this.btn_newGameComputer.Click += new System.EventHandler(this.btn_newGameComputer_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 478);
            this.Controls.Add(this.btn_newGameComputer);
            this.Controls.Add(this.lbl_tips);
            this.Controls.Add(this.cb_tips);
            this.Controls.Add(this.lbl_NextMove);
            this.Controls.Add(this.lbl_secondPlayerScore);
            this.Controls.Add(this.lbl_firstPlayerScore);
            this.Controls.Add(this.btn_newGame);
            this.Controls.Add(this.pnl_Field);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(358, 520);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MegaReversi";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnl_Field;
        private System.Windows.Forms.Button btn_newGame;
        private System.Windows.Forms.Label lbl_firstPlayerScore;
        private System.Windows.Forms.Label lbl_secondPlayerScore;
        private System.Windows.Forms.Label lbl_NextMove;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menu_NewGame;
        private System.Windows.Forms.ToolStripMenuItem saveGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadLastGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openDialog;
        private System.Windows.Forms.SaveFileDialog saveDialog;
        private System.Windows.Forms.CheckBox cb_tips;
        private System.Windows.Forms.Label lbl_tips;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button btn_newGameComputer;
        private System.Windows.Forms.ToolStripMenuItem newComputerGameToolStripMenuItem;
    }
}

