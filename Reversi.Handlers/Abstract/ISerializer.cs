using Reversi.GameEngine;

namespace Reversi.Handlers.Interfaces
{
    interface ISerializer
    {
        void Serialize(GameState state, string fileName);
        GameState Deserialize(string fileName);
    }
}
