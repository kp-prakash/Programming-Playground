using MineSweeper.Shared;

namespace MineSweeper.Model
{
    // Represents a single cell
    public class Cell
    {
        /// <summary>
        /// Indicates the state of the cell.
        /// </summary>
        /// <value>
        /// The state of the cell.
        /// </value>
        public CellState CellState { get; set; }

        /// <summary>
        /// Indicates the mine / mine count.
        /// If -1 it is mine and if it is positive integer it indicates the number of mines around it.
        /// </summary>
        /// <value>
        /// The mine.
        /// </value>
        public int Mine { get; set; }
    }
}