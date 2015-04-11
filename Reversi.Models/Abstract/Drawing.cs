namespace Reversi.GameEngine
{
    public abstract class Drawing
    {
        #region Properties
        public Field GameField { get; set; }
        #endregion

        #region Methods
        public void DrawField(int player, bool enabledTips)
        {
            Draw(player, enabledTips);
            DrawEnableMoves(player, enabledTips);
        }
        #endregion

        #region Abstract Methods
        abstract protected void Draw(int player, bool enabledTips);
        abstract protected void DrawEnableMoves(int player, bool enabledTips);
        #endregion
    }
}
