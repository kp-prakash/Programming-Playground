using MineSweeper.Model;

namespace MineSweeper.Shared.Interfaces
{
    //TODO: Move this to a separate project "MineSweeper.Shared".
    /// <summary>
    /// Represents a mine field and its responsibilities.
    /// </summary>
    public interface IField
    {
        /// <summary>
        /// Gets the cells.
        /// </summary>
        /// <value>
        /// The cells.
        /// </value>
        Cell[,] Cells { get; }

        /// <summary>
        /// Gets the columns.
        /// </summary>
        /// <value>
        /// The columns.
        /// </value>
        int Columns { get; }

        /// <summary>
        /// Gets the rows.
        /// </summary>
        /// <value>
        /// The rows.
        /// </value>
        int Rows { get; }

        /// <summary>
        /// Gets the total mines.
        /// </summary>
        /// <value>
        /// The total mines.
        /// </value>
        int TotalMines { get; }

        /// <summary>
        /// Flag a row and column
        /// </summary>
        /// <param name="row">Row</param>
        /// <param name="column">Column</param>
        void Flag(int row, int column);

        /// <summary>
        /// Opens a row and column and returns the number of adjacent mines.
        /// </summary>
        /// <param name="row">Row</param>
        /// <param name="column">Column</param>
        /// <returns>Number of adjacent mines.</returns>
        void Open(int row, int column);

        /// <summary>
        /// Reveals the solution if you are unable to crack this!
        /// </summary>
        /// <returns>FieldMap</returns>
        Cell[,] RevealSolution();
    }
}