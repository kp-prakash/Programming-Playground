using System;
using System.Globalization;
using MineSweeper.Model;
using MineSweeper.Shared;
using MineSweeper.Shared.Exceptions;
using MineSweeper.Utils;

namespace MineSweeper
{
    /// <summary>
    /// Console Application Entry Point
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        public static void Main()
        {
            var field = InitializeField();
            if (null == field)
            {
                // Layout initialization failed!
                Console.ReadKey();
                return;
            }

            while (true)
            {
                PrintField(field);
                Console.WriteLine("Press F to Flag, O to Open, R to Reveal, E to Exit:");
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                Console.WriteLine();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.F:
                        FlagFieldCell(field);
                        break;

                    case ConsoleKey.O:
                        bool doExit = OpenFieldCellCell(field);
                        if (doExit)
                        {
                            Exit();
                            return;
                        }
                        break;

                    case ConsoleKey.R:
                        field.RevealSolution();
                        PrintField(field);
                        Console.WriteLine("You got the solution! Good Bye!");
                        Console.ReadKey();
                        return;

                    case ConsoleKey.E:
                        Exit();
                        return;

                    default:
                        Console.WriteLine("Press a valid key!");
                        break;
                }
                Console.Clear();
            }
        }

        /// <summary>
        /// Prepares console for exit.
        /// </summary>
        private static void Exit()
        {
            Console.Clear();
            Console.WriteLine("Good Bye!");
            Console.ReadKey();
        }

        /// <summary>
        /// Flags the field cell.
        /// </summary>
        /// <param name="field">The field.</param>
        private static void FlagFieldCell(Field field)
        {
            try
            {
                Coordinates coordinates = GetCoordinates();
                field.Flag(coordinates.Row, coordinates.Column);
            }
            catch (MineSweeperException mineSweeperException)
            {
                Console.WriteLine(mineSweeperException.Message);
            }
        }

        /// <summary>
        /// Gets the state of the cell.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <returns></returns>
        private static string GetCellState(Cell cell)
        {
            string result = string.Empty;
            switch (cell.CellState)
            {
                case CellState.Closed:
                    result = Settings.Default.Closed;
                    break;

                case CellState.Flagged:
                    result = Settings.Default.Flagged;
                    break;

                case CellState.Opened:
                    result = cell.Mine == Settings.Default.MineIndicator
                                ? Settings.Default.Mine.ToString(CultureInfo.InvariantCulture)
                                : cell.Mine.ToString(CultureInfo.InvariantCulture);
                    break;
            }
            return result;
        }

        /// <summary>
        /// Gets the column.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="MineSweeperException">Please enter a valid column!</exception>
        private static int GetColumn()
        {
            Console.WriteLine("Please enter column value:");
            string c = Console.ReadLine();
            int column;
            if (!int.TryParse(c, out column))
            {
                throw new MineSweeperException("Please enter a valid column!");
            }
            return column;
        }

        /// <summary>
        /// Gets the coordinates.
        /// </summary>
        /// <returns></returns>
        private static Coordinates GetCoordinates()
        {
            var coordinates = new Coordinates
            {
                Row = GetRow(),
                Column = GetColumn()
            };
            return coordinates;
        }

        /// <summary>
        /// Gets the row.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="MineSweeperException">Please enter a valid row!</exception>
        private static int GetRow()
        {
            Console.WriteLine("Please enter row value:");
            string r = Console.ReadLine();
            int row;
            if (!int.TryParse(r, out row))
            {
                throw new MineSweeperException("Please enter a valid row!");
            }
            return row;
        }

        /// <summary>
        /// Initializes the field.
        /// </summary>
        /// <returns></returns>
        private static Field InitializeField()
        {
            try
            {
                // Choose between auto and manual initialization.
                // var field = new Field(Settings.Default.SampleLayout);
                var random = RandomGenerator.GetRandomNumberGenerator();
                var field = new Field(random.Next(Settings.Default.MinimumRows, Settings.Default.MaximumRows)
                                    , random.Next(Settings.Default.MinimumColumns, Settings.Default.MaximumColumns));
                return field;
            }
            catch (MineSweeperException mineSweeperException)
            {
                Console.WriteLine(mineSweeperException.Message);
                return null;
            }
        }

        /// <summary>
        /// Opens the field cell cell.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        private static bool OpenFieldCellCell(Field field)
        {
            bool doExit = false;
            try
            {
                Coordinates coordinates = GetCoordinates();
                field.Open(coordinates.Row, coordinates.Column);
            }
            catch (GameOverException gameOverException)
            {
                Console.WriteLine(gameOverException.Message);
                doExit = true;
            }
            catch (MineSweeperException mineSweeperException)
            {
                Console.WriteLine(mineSweeperException.Message);
            }
            return doExit;
        }

        /// <summary>
        /// Prints the field map.
        /// </summary>
        /// <param name="field">The field.</param>
        private static void PrintField(Field field)
        {
            for (int row = 0; row < field.Rows; row++)
            {
                for (int column = 0; column < field.Columns; column++)
                {
                    Console.Write("{0} ", GetCellState(field.Cells[row, column]));
                }
                Console.WriteLine();
            }
        }
    }
}