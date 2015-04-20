namespace Reversi.GameEngine
{
    public class GameState
    {
        public GameState()
        {
            Field = new Field();
        }
        public Field Field { get; set; }
        public bool EnabledTips { get; set; }
        public int CurrentMove { get; set; }
        public bool FirstMoveAI { get; set; }
    }
}
