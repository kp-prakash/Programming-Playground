using Shared;

namespace Entities
{
    public class Cell : ICell
    {
        public bool IsAlive { get; private set; }

        public void Clear()
        {
            IsAlive = false;
        }

        public char GetState()
        {
            return IsAlive ? Constants.Alive : Constants.Dead;
        }

        public void Toggle()
        {
            IsAlive = !IsAlive;
        }
    }
}