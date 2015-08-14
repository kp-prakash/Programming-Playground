namespace Shared
{
    public class RuleEngine
    {
        public static bool WillBeAlive(int numberOfAliveCells, bool isAlive)
        {
            return isAlive ? CanStayAlive(numberOfAliveCells) : CanRejuvenate(numberOfAliveCells);
        }

        private static bool CanRejuvenate(int numberOfAliveCells)
        {
            return numberOfAliveCells == Constants.MaxAliveNeighbours;
        }

        private static bool CanStayAlive(int numberOfAliveCells)
        {
            return numberOfAliveCells == Constants.MinAliveNeighbours 
                   || numberOfAliveCells == Constants.MaxAliveNeighbours;
        }
    }
}