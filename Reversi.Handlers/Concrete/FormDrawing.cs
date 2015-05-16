using System;
using System.Windows.Forms;
using System.Drawing;
using Reversi.GameEngine;

namespace Reversi.Handlers
{
    public sealed class FormDrawing : Drawing
    {
        #region Variabels
        private Panel _panel;
        private Bitmap _imgForFirstPlayer;
        private Bitmap _imgForSecondPlayer;
        #endregion

        #region Constructors
        public FormDrawing(Panel panel, Field gameField)
        {
            _imgForFirstPlayer = new Bitmap(Reversi.Handlers.Properties.Resources.red);
            _imgForSecondPlayer = new Bitmap(Reversi.Handlers.Properties.Resources.blue);
            _panel = panel;
            GameField = gameField;
        }
        #endregion

        #region Overrided abstracts methods
        protected override void Draw(int player, bool enabledTips)
        {
            Graphics graphics = Graphics.FromHwnd(_panel.Handle);
            Pen p = new Pen(Color.Black, (float)0.5);
            Bitmap image;
            for (int i = 0; i < Field.N; i++)
            {
                for (int j = 0; j < Field.N; j++)
                {
                    Color c = Color.FromArgb(136, 210, 210);
                    //draw lines
                    graphics.FillRectangle(new SolidBrush(c), j * Field.SCALE, i * Field.SCALE, Field.SCALE, Field.SCALE);
                    graphics.DrawRectangle(p, j * Field.SCALE, i * Field.SCALE, Field.SCALE, Field.SCALE);
                    //draw players play-chip                    
                    if (GameField[i, j] == 0)
                    {
                        c = Color.White;
                    }
                    else
                    {
                        if (GameField[i, j] == (int)Players.FirstPlayer)
                        {
                            c = Color.Red;
                            image = _imgForFirstPlayer;
                        }
                        else
                        {
                            c = Color.Blue;
                            image = _imgForSecondPlayer;
                        }
                        try
                        {
                            graphics.DrawImage(image, new Rectangle(j * Field.SCALE + 3, i * Field.SCALE + 3, Field.SCALE - 6, Field.SCALE - 6));
                        }
                        catch (InvalidOperationException ex)
                        {
                            if (GameField[i, j] == (int)Players.FirstPlayer)
                            {
                                image = new Bitmap(Reversi.Handlers.Properties.Resources.red);
                            }
                            else
                            {
                                image = new Bitmap(Reversi.Handlers.Properties.Resources.blue);
                            }
                            graphics.DrawImage(image, new Rectangle(j * Field.SCALE + 3, i * Field.SCALE + 3, Field.SCALE - 6, Field.SCALE - 6));
                            MessageBox.Show("Not so fast");
                        }
                    }
                }
            }
        }

        protected override void DrawEnableMoves(int player, bool enabledTips)
        {
            Graphics graphics = Graphics.FromHwnd(_panel.Handle);
            Pen pen;            
            if (player == (int)Players.FirstPlayer)
            {
                pen = new Pen(Color.FromArgb(220, 42, 71), (float)2.8);
            }
            else
            {
                pen = new Pen(Color.FromArgb(42, 94, 146), (float)2.8);
            }

            if (enabledTips && GameField.GameProcess)
            {
                foreach (Reversi.GameEngine.Point point in GameField.MovePoints.Keys)
                {
                    graphics.DrawRectangle(pen, point.Y * Field.SCALE + 3, point.X * Field.SCALE + 3, Field.SCALE - 6, Field.SCALE - 6);
                }
            }
        }
        #endregion
    }
}
