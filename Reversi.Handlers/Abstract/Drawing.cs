using Reversi.GameEngine;
using Reversi.GameEngine.Classes;

namespace Reversi.Handlers
{
    public abstract class Drawing
    {
        #region Properties
        public Field GameField { get; set; }
        #endregion

        #region Methods
        public void DrawField(object sender, DrawEventArgs args)
        {
            Draw(args.Player, args.Tips);
            DrawEnableMoves(args.Player, args.Tips);
        }
        #endregion

        #region Abstract Methods
        abstract protected void Draw(int player, bool enabledTips);
        abstract protected void DrawEnableMoves(int player, bool enabledTips);
        #endregion
    }
}
