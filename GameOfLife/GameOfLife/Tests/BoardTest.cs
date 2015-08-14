using System;
using System.Linq;
using Entities;
using NUnit.Framework;
using Shared;
using Tests.Properties;

namespace Tests
{
    [TestFixture]
    public class BoardTest
    {
        [Test]
        public void TestBoardBounds()
        {
            var board = Factory.Instance.GetNewBoard(2, 2);
            var outOfBoundCell = board[10, 10];
            Assert.True(null == outOfBoundCell);
        }

        [Test]
        public void TestBoardConstruction()
        {
            var board = Factory.Instance.GetNewBoard(2, 2);
            var cell = board[0, 0];
            Assert.True(null != cell);
            if (null == cell)
                throw new NullReferenceException(Resources.NullReferenceException);
            var isAlive = cell.IsAlive;
            Assert.True(!isAlive);
        }

        [Test]
        public void TestIsCellReincarnatedNegative()
        {
            var board = Factory.Instance.GetNewBoard(2, 2);
            var isCellReIncarnated = board.IsCellReincarnated(0, 0);
            Assert.True(!isCellReIncarnated);
        }

        [Test]
        public void TestIsCellReincarnatedPositive()
        {
            var board = Factory.Instance.GetNewBoard(2, 2);
            board[0, 0].Toggle();
            board[0, 1].Toggle();
            board[1, 0].Toggle();
            var isCellReIncarnated = board.IsCellReincarnated(1, 1);
            Assert.True(isCellReIncarnated);
        }

        [Test]
        public void TestSize()
        {
            var board = Factory.Instance.GetNewBoard(2, 2);
            Assert.True(board.RowCount.Equals(2)
                && board.ColumnCount.Equals(2));
        }

        [Test]
        public void TestToString()
        {
            var board = Factory.Instance.GetNewBoard(2, 2);
            const string expected = "--\n--";
            var actual = board.ToString();
            Assert.True(expected.Equals(actual));
        }

        [Test]
        public void TestConsecutiveCellCountNegative()
        {
            var board = Factory.Instance.GetNewBoard(2, 2);
            var topCount = board.GetConsecutiveHorizontalCellCount(Borders.Top);
            var bottomCount = board.GetConsecutiveHorizontalCellCount(Borders.Bottom);
            var leftCount = board.GetConsecutiveVerticalCellCount(Borders.Left);
            var rightCount = board.GetConsecutiveVerticalCellCount(Borders.Right);
            Assert.True((topCount==0) && (bottomCount==0)&& (leftCount==0)&& (rightCount==0));
        }

        [Test]
        public void TestConsecutiveCellCountPositive()
        {
            var board = Factory.Instance.GetNewBoard(3, 3);
            ToggleAllCells(board);
            var topCount = board.GetConsecutiveHorizontalCellCount(Borders.Top);
            var bottomCount = board.GetConsecutiveHorizontalCellCount(Borders.Bottom);
            var leftCount = board.GetConsecutiveVerticalCellCount(Borders.Left);
            var rightCount = board.GetConsecutiveVerticalCellCount(Borders.Right);
            Assert.True((topCount == 1) && (bottomCount == 1) && (leftCount == 1) && (rightCount == 1));
        }

        private void ToggleAllCells(IBoard board)
        {
            var validCells = from row in board.Rows
                             where null != row
                             from cell in row.Cells
                             where null != cell
                             select cell;
            foreach (var cell in validCells)
            {
                cell.Toggle();
            }
        }

        [Test]
        public void TestAddRowAtTop()
        {
            TestAddRow(Borders.Top);
        }

        [Test]
        public void TestAddRowAtBottom()
        {
            TestAddRow(Borders.Bottom);
        }

        private void TestAddRow(Borders border)
        {
            const int expectedOldRowCount = 3;
            const int expectedNewRowCount = 4;
            var board = Factory.Instance.GetNewBoard(3, 3);
            var oldRowCount = board.RowCount;
            board.AddRow(border);
            var newRowCount = board.RowCount;
            Assert.True(expectedOldRowCount.Equals(oldRowCount)
                        && expectedNewRowCount.Equals(newRowCount));
        }

        [Test]
        public void TestAddColumnToLeft()
        {
            TestAddColumn(Borders.Left);
        }

        [Test]
        public void TestAddColumnToRight()
        {
            TestAddColumn(Borders.Right);
        }

        private void TestAddColumn(Borders border)
        {
            const int expectedOldColumnCount = 3;
            const int expectedNewColumnCount = 4;
            var board = Factory.Instance.GetNewBoard(3, 3);
            var oldColumnCount = board.ColumnCount;
            board.AddColumn(border);
            var newColumnCount = board.ColumnCount;
            Assert.True(expectedOldColumnCount.Equals(oldColumnCount)
                        && expectedNewColumnCount.Equals(newColumnCount));
        }
    }
}