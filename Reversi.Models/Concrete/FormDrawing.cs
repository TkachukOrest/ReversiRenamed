using System;
using System.Windows.Forms;
using System.Drawing;

namespace Reversi.GameEngine
{
    public sealed class FormDrawing:Drawing
    {
        #region Variabels
        private Panel _panel;
        private Bitmap _imgForFirstPlayer;
        private Bitmap _imgForSecondPlayer;
        #endregion

        #region Constructors
        public FormDrawing(Panel panel,Field gameField)
        {
            _imgForFirstPlayer = new Bitmap(Reversi.GameEngine.Properties.Resources.red);
            _imgForSecondPlayer = new Bitmap(Reversi.GameEngine.Properties.Resources.blue);
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
                    graphics.FillRectangle(new SolidBrush(c), j * Field.Scale, i * Field.Scale, Field.Scale, Field.Scale);
                    graphics.DrawRectangle(p, j * Field.Scale, i * Field.Scale, Field.Scale, Field.Scale);
                    //draw players play-chip                    
                    if (GameField[i, j] == 0)
                    {
                        c = Color.White;
                    }
                    else
                    {
                        if (GameField[i, j] == 1)
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
                            graphics.DrawImage(image, new Rectangle(j * Field.Scale + 3, i * Field.Scale + 3, Field.Scale - 6, Field.Scale - 6));
                        }
                        catch(InvalidOperationException ex)
                        {
                            if (GameField[i, j] == 1)
                            {
                                image = new Bitmap(Reversi.GameEngine.Properties.Resources.red);
                            }
                            else
                            {
                                image = new Bitmap(Reversi.GameEngine.Properties.Resources.blue);
                            }
                            graphics.DrawImage(image, new Rectangle(j * Field.Scale + 3, i * Field.Scale + 3, Field.Scale - 6, Field.Scale - 6));
                            MessageBox.Show("Не так швидко)");                            
                        }

                    }
                }
            }            
        }

        protected override void DrawEnableMoves(int player, bool enabledTips)
        {
            Graphics graphics = Graphics.FromHwnd(_panel.Handle);            
            Pen pen;
            if (player == 1)
                pen = new Pen(Color.FromArgb(220, 42, 71), (float)2.8);
            else
                pen = new Pen(Color.FromArgb(42, 94, 146), (float)2.8);

            GameField.FindEnabledMoves(player);

            if (enabledTips&&GameField.GameProcess)
            {
                foreach(Point point in GameField.MovePoints.Keys)
                        {                                              
                             graphics.DrawRectangle(pen, point.Y * Field.Scale + 3, point.X * Field.Scale + 3, Field.Scale - 6, Field.Scale - 6);
                         }
            }                                    
        }
        #endregion
    }
}
