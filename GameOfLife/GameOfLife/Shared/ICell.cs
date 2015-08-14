namespace Shared
{
    public interface ICell
    {
        bool IsAlive { get; }
        void Clear();
        char GetState();
        void Toggle();
    }
}