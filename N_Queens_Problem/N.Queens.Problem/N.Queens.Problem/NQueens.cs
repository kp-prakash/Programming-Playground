using System;

namespace N.Queens.Problem
{
    public class NQueens
    {
        private readonly int[] _queens;
        private readonly int _boardSize;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="boardSize">Size of the board</param>
        public NQueens(int boardSize)
        {
            _boardSize = boardSize;
            _queens = new int[_boardSize];
        }

        /// <summary>
        /// One dimensional array for board.
        /// If Queens[r] = c indicates that there 
        /// is a queen in row r and column c.
        /// </summary>
        public int[] Queens
        {
            get { return _queens; }
        }

        /// <summary>
        /// Board Size
        /// </summary>
        public int BoardSize
        {
            get { return _boardSize; }
        }

        /// <summary>
        /// Prints all possible solution to N Queens problem to console.
        /// </summary>
        /// <param name="initialRow">Row to start with, usually 0.</param>
        /// <param name="count">Indicates the total number of solutions</param>
        public void ResolveNQueens(int initialRow,ref int count)
        {
           for (int columnIndex = 0; columnIndex < BoardSize; columnIndex++)
            {
                if (IsValidPosition(initialRow, columnIndex))
                {
                    Queens[initialRow] = columnIndex;
                    //Check if every row has a queen print the solution
                    //and proceed to next possible solution.
                    //else continue with next row.
                    if (initialRow == BoardSize - 1)
                    {
                        count++;
                        PrintBoard(ref count);
                    }
                    else
                    {
                        ResolveNQueens(initialRow + 1, ref count);
                    }
                }
               //If no valid position is found, then the algorithm backtracks 
               //to the current row and tries placing the queen in next column.
            }
        }

        /// <summary>
        /// Prints the board as a matrix
        /// </summary>
        /// <param name="count">Indicates the solution count.</param>
        private void PrintBoard(ref int count)
        {
            Console.WriteLine("Solution number {0} for {1} Queen's Problem.", count, BoardSize);
            for (int rowIndex = 0; rowIndex < BoardSize; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < BoardSize; columnIndex++)
                {
                    Console.Write(Queens[rowIndex] == columnIndex ? 'Q' : ' ');
                }
                Console.WriteLine();
            }
            for (int i = 0; i < BoardSize; i++)
            {
                Console.Write('-');
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Determines if the position is valid.
        /// </summary>
        /// <param name="row">New position's row</param>
        /// <param name="column">New position's Column</param>
        /// <returns>Can the queen be placed in given position?</returns>
        private bool IsValidPosition(int row, int column)
        {
            for (int rowIndex = 0; rowIndex < row; rowIndex++)
            {
                //There should be no queens in the row / column / diagonals.
                if (Queens[rowIndex] == column
                    || Math.Abs(row - rowIndex) == Math.Abs(column - Queens[rowIndex]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}