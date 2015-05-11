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
            this.pnlField = new System.Windows.Forms.Panel();
            this.btnNewGame = new System.Windows.Forms.Button();
            this.lblFirstPlayerScore = new System.Windows.Forms.Label();
            this.lblSecondPlayerScore = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_NewGame = new System.Windows.Forms.ToolStripMenuItem();
            this.newComputerGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tipsOnOffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.saveGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadLastGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveDialog = new System.Windows.Forms.SaveFileDialog();
            this.btnNewGameComputer = new System.Windows.Forms.Button();
            this.pbRed = new System.Windows.Forms.PictureBox();
            this.pbBlue = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBlue)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlField
            // 
            this.pnlField.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.pnlField.Location = new System.Drawing.Point(8, 130);
            this.pnlField.Name = "pnlField";
            this.pnlField.Size = new System.Drawing.Size(321, 321);
            this.pnlField.TabIndex = 0;
            this.pnlField.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlField_Paint);
            this.pnlField.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlField_MouseClick);
            // 
            // btnNewGame
            // 
            this.btnNewGame.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnNewGame.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNewGame.Location = new System.Drawing.Point(8, 68);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(321, 23);
            this.btnNewGame.TabIndex = 0;
            this.btnNewGame.Text = "New game with player";
            this.btnNewGame.UseVisualStyleBackColor = false;
            this.btnNewGame.Click += new System.EventHandler(this.btnNewGame_Click);
            // 
            // lblFirstPlayerScore
            // 
            this.lblFirstPlayerScore.AutoSize = true;
            this.lblFirstPlayerScore.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblFirstPlayerScore.Location = new System.Drawing.Point(132, 40);
            this.lblFirstPlayerScore.Name = "lblFirstPlayerScore";
            this.lblFirstPlayerScore.Size = new System.Drawing.Size(22, 13);
            this.lblFirstPlayerScore.TabIndex = 1;
            this.lblFirstPlayerScore.Text = "11:";
            // 
            // lblSecondPlayerScore
            // 
            this.lblSecondPlayerScore.AutoSize = true;
            this.lblSecondPlayerScore.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblSecondPlayerScore.Location = new System.Drawing.Point(211, 40);
            this.lblSecondPlayerScore.Name = "lblSecondPlayerScore";
            this.lblSecondPlayerScore.Size = new System.Drawing.Size(25, 13);
            this.lblSecondPlayerScore.TabIndex = 2;
            this.lblSecondPlayerScore.Text = " 22:";
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
            this.tipsOnOffToolStripMenuItem,
            this.toolStripMenuItem3,
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
            // tipsOnOffToolStripMenuItem
            // 
            this.tipsOnOffToolStripMenuItem.Name = "tipsOnOffToolStripMenuItem";
            this.tipsOnOffToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.tipsOnOffToolStripMenuItem.Text = "Tips On/Off";
            this.tipsOnOffToolStripMenuItem.Click += new System.EventHandler(this.tipsOnOffToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(183, 6);
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
            // btnNewGameComputer
            // 
            this.btnNewGameComputer.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnNewGameComputer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNewGameComputer.Location = new System.Drawing.Point(8, 97);
            this.btnNewGameComputer.Name = "btnNewGameComputer";
            this.btnNewGameComputer.Size = new System.Drawing.Size(321, 23);
            this.btnNewGameComputer.TabIndex = 7;
            this.btnNewGameComputer.Text = "New game with computer";
            this.btnNewGameComputer.UseVisualStyleBackColor = false;
            this.btnNewGameComputer.Click += new System.EventHandler(this.btnNewGameComputer_Click);
            // 
            // pbRed
            // 
            this.pbRed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbRed.Image = global::Reversi.Properties.Resources.red;
            this.pbRed.Location = new System.Drawing.Point(102, 32);
            this.pbRed.Name = "pbRed";
            this.pbRed.Size = new System.Drawing.Size(24, 21);
            this.pbRed.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbRed.TabIndex = 8;
            this.pbRed.TabStop = false;
            // 
            // pbBlue
            // 
            this.pbBlue.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbBlue.Image = global::Reversi.Properties.Resources.blue2;
            this.pbBlue.Location = new System.Drawing.Point(181, 32);
            this.pbBlue.Name = "pbBlue";
            this.pbBlue.Size = new System.Drawing.Size(24, 21);
            this.pbBlue.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbBlue.TabIndex = 9;
            this.pbBlue.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 460);
            this.Controls.Add(this.pbBlue);
            this.Controls.Add(this.pbRed);
            this.Controls.Add(this.btnNewGameComputer);
            this.Controls.Add(this.lblSecondPlayerScore);
            this.Controls.Add(this.lblFirstPlayerScore);
            this.Controls.Add(this.btnNewGame);
            this.Controls.Add(this.pnlField);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(354, 494);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MegaReversi";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBlue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlField;
        private System.Windows.Forms.Button btnNewGame;
        private System.Windows.Forms.Label lblFirstPlayerScore;
        private System.Windows.Forms.Label lblSecondPlayerScore;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menu_NewGame;
        private System.Windows.Forms.ToolStripMenuItem saveGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadLastGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openDialog;
        private System.Windows.Forms.SaveFileDialog saveDialog;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button btnNewGameComputer;
        private System.Windows.Forms.ToolStripMenuItem newComputerGameToolStripMenuItem;
        private System.Windows.Forms.PictureBox pbRed;
        private System.Windows.Forms.PictureBox pbBlue;
        private System.Windows.Forms.ToolStripMenuItem tipsOnOffToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
    }
}

