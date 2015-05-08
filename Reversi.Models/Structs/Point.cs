namespace Reversi.GameEngine
{
    public struct Point
    {
        private int _x;
        private int _y;
        public int X { get { return _x; } }
        public int Y { get { return _y; } }

        public Point(int x, int y)
        {
            _x = x;
            _y = y;
        }
    }

}
