using System.Collections.Generic;
using System.Linq;
using MineSweeper.Model;
using MineSweeper.Shared;
using MineSweeper.Shared.Exceptions;

namespace MineSweeper.Utils
{
    public class FieldMapFactory
    {
        /// <summary>
        /// Generates a random fieldmap given the number of rows and columns.
        /// </summary>
        /// <param name="rows">The rows.</param>
        /// <param name="columns">The columns.</param>
        /// <returns>FieldMap</returns>
        public static Cell[,] GetFieldMap(int rows, int columns)
        {
            var random = RandomGenerator.GetRandomNumberGenerator(rows, columns);
            var cells = new Cell[rows, columns];
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    cells[row, column] = new Cell
                    {
                        CellState = CellState.Closed,
                        Mine = Settings.Default.DefaultIndicator
                    };
                }

                // Randomly compute the number of mines per row and the mine locations in a row.
                int numberOfminesPerRow = random.Next(columns / 2);
                for (int i = 0; i < numberOfminesPerRow; i++)
                {
                    int mineLocation = random.Next(1, columns);
                    // Trying to force the number of unique random mines takes more time as
                    // random number generator should genrate something in a finite set.
                    // while (cells[row, i].Mine == Settings.Default.MineIndicator)
                    // {
                    //     mineLocation = random.Next(1, columns);
                    // }
                    cells[row, mineLocation].Mine = Settings.Default.MineIndicator;
                }
            }
            return cells;
        }

        /// <summary>
        /// Gets the field map for the user defined mine layout.
        /// </summary>
        /// <param name="mineLayout">The mine layout.</param>
        /// <returns></returns>
        public static Cell[,] GetFieldMap(string mineLayout)
        {
            char[,] layout = ProcessMineLayout(mineLayout);
            int rows = layout.GetLength(0);
            int columns = layout.GetLength(1);
            var cells = new Cell[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    // Initializes cell based on the layout.
                    cells[i, j] = new Cell
                    {
                        CellState = CellState.Closed,
                        Mine = layout[i, j] == Settings.Default.Mine
                                                  ? Settings.Default.MineIndicator
                                                  : Settings.Default.DefaultIndicator
                    };
                }
            }

            return cells;
        }

        /// <summary>
        /// Processes the mine layout.
        /// </summary>
        /// <param name="mineLayout">The uni dimensional comma separated mine layout.</param>
        /// <returns>2 dimensional mine layout.</returns>
        /// <exception cref="MineSweeperException">Invalid layout</exception>
        private static char[,] ProcessMineLayout(string mineLayout)
        {
            string[] rows = mineLayout.Split(',');
            int rowCount = rows.Length;
            if (!ValidateLayout(rows) && rowCount > 0)
            {
                throw new MineSweeperException("Invalid layout");
            }

            var layout = new char[rows.Length, rows[0].Length];
            for (int row = 0; row < layout.GetLength(0); row++)
            {
                for (int column = 0; column < layout.GetLength(1); column++)
                {
                    layout[row, column] = rows[row][column];
                }
            }
            return layout;
        }

        /// <summary>
        /// Validates the layout.
        /// </summary>
        /// <param name="rows">The rows.</param>
        /// <returns></returns>
        private static bool ValidateLayout(IEnumerable<string> rows)
        {
            return rows.GroupBy(row => row.Length).Count() == 1;
        }
    }
}