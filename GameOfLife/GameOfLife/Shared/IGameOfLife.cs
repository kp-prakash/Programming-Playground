using System;

namespace Shared
{
    public interface IGameOfLife
    {
        int ColumnCount { get; }
        int RowCount { get; }
        int Generation { get; }
        IBoard CurrentGeneration { get; }
        IBoard NextGeneration { get; }
        void ProcessGeneration();
        event Action<int, IBoard> OnGenerationCompleted;
    }
}