using MineSweeper.Shared;
using MineSweeper.Shared.Exceptions;
using MineSweeper.Shared.Interfaces;
using MineSweeper.Utils;

namespace MineSweeper.Model
{
    /// <summary>
    /// Represents the mine sweeper field.
    /// </summary>
    public class Field : IField
    {
        /// <summary>
        /// Auto initializes a new instance of the <see cref="Field"/> class.
        /// </summary>
        /// <param name="rows">The rows.</param>
        /// <param name="columns">The columns.</param>
        public Field(int rows, int columns)
        {
            Cells = FieldMapFactory.GetFieldMap(rows, columns);
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Field"/> class with the given layout.
        /// </summary>
        /// <param name="mineLayout">The mine layout.</param>
        public Field(string mineLayout)
        {
            Cells = FieldMapFactory.GetFieldMap(mineLayout);
            Initialize();
        }

        /// <summary>
        /// Gets the cells.
        /// </summary>
        /// <value>
        /// The cells.
        /// </value>
        public Cell[,] Cells { get; private set; }

        /// <summary>
        /// Gets the columns.
        /// </summary>
        /// <value>
        /// The columns.
        /// </value>
        public int Columns { get; private set; }

        /// <summary>
        /// Gets the rows.
        /// </summary>
        /// <value>
        /// The rows.
        /// </value>
        public int Rows { get; private set; }

        /// <summary>
        /// Gets the total mines.
        /// </summary>
        /// <value>
        /// The total mines.
        /// </value>
        public int TotalMines { get; private set; }

        /// <summary>
        /// Flag a row and column
        /// </summary>
        /// <param name="row">Row</param>
        /// <param name="column">Column</param>
        public void Flag(int row, int column)
        {
            Validate(row, column);
            Cell cell = Cells[row, column];
            cell.CellState = CellState.Flagged;
            // Decrement the total number of mines if this flagged cell was a mine,
            // to indicate to user and finish the game once all mines are flagged successfully.
        }

        /// <summary>
        /// Opens a row and column and returns the number of adjacent mines.
        /// </summary>
        /// <param name="row">Row</param>
        /// <param name="column">Column</param>
        /// <exception cref="MineSweeper.Shared.Exceptions.GameOverException">Game Over!</exception>
        public void Open(int row, int column)
        {
            Validate(row, column);
            Cell cell = Cells[row, column];
            if (cell.Mine == -1)
            {
                throw new GameOverException("Game Over!");
            }

            cell.CellState = CellState.Opened;
        }

        /// <summary>
        /// Reveals the solution if you are unable to crack this!
        /// </summary>
        /// <returns>
        /// FieldMap
        /// </returns>
        public Cell[,] RevealSolution()
        {
            //Better to copy the array to a new array and return that as revealed result.
            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    Cells[row, column].CellState = CellState.Opened;
                }
            }
            return Cells; //This return statement is to return the cloned array.
        }

        /// <summary>
        /// Computes the number of mines adjacent to a cell and total number of mines.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="MineSweeper.Shared.Exceptions.MineSweeperException">Field not initialized exception!</exception>
        private int ComputeNumberOfMinesAdjacentToACellAndTotalNumberOfMines()
        {
            if (null == Cells)
            {
                throw new MineSweeperException("Field not initialized exception!");
            }
            int mineCount = 0;
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Cell cell = Cells[i, j];
                    if (cell.Mine == -1)
                    {
                        mineCount++;
                        continue;
                    }
                    cell.Mine = CountMinesAroundACell(i, j);
                }
            }

            return mineCount;
        }

        /// <summary>
        /// Counts the mines around a cell.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <returns>Number of mines</returns>
        private int CountMinesAroundACell(int row, int column)
        {
            int count = 0;
            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = column - 1; j <= column + 1; j++)
                {
                    if (i == row && j == column)
                    {
                        continue;
                    }
                    if (i >= 0 && i < Rows && j >= 0 && j < Columns && Cells[i, j].Mine == -1)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            Rows = Cells.GetLength(0);
            Columns = Cells.GetLength(1);
            TotalMines = ComputeNumberOfMinesAdjacentToACellAndTotalNumberOfMines();
        }

        /// <summary>
        /// Validates the specified row.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <exception cref="MineSweeperException">Invalid row and column!</exception>
        private void Validate(int row, int column)
        {
            if (row >= Rows || column >= Columns)
            {
                throw new MineSweeperException("Invalid row and column!");
            }
        }
    }
}