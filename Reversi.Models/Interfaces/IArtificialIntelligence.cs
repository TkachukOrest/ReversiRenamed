namespace Reversi.GameEngine
{
    public interface IArtificialIntelligence
    {
        void DoComputerMove(Field field, int player);
        Point RandomStep(Field field, out int count);
        Point BestStep(Field gameField, int player);
    }
}
