using System;

namespace Shared
{
    public interface IInputParser
    {
        IGameOfLife CurrentGame { get; }
        void InitializeGame(string input, Action<int, IBoard> notificationCallBack = null);
        void ProcessFirstGeneration();
        void StartGame();
    }
}