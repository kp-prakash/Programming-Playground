using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Entities
{
    public class Board : IBoard
    {
        public IList<IRow> Rows { get; private set; }

        public int RowCount
        {
            get { return (null != Rows) ? Rows.Count : 0; }
        }

        public int ColumnCount
        {
            get { return (null != Rows && Rows.Count > 0 && null != Rows[0]) ? Rows[0].ColumnCount : 0; }
        }

        public Board(int rowCount, int columnCount)
        {
            Rows = Factory.Instance.GetNewRows(rowCount, columnCount);
        }

        public ICell this[int x, int y]
        {
            get { return IsValid(x, y) ? Rows[x][y] : null; }
            set { if (IsValid(x, y)) Rows[x][y] = value; }
        }

        public void AddColumn(Borders border)
        {
            foreach (var cells in Rows.Where(row => null != row).Select(row => row.Cells).Where(cells => null != cells))
            {
                switch (border)
                {
                    case Borders.Left:
                        cells.Insert(0, Factory.Instance.GetNewCell());
                        break;
                    case Borders.Right:
                        cells.Add(Factory.Instance.GetNewCell());
                        break;
                }
            }
        }

        public void AddRow(Borders border)
        {
            switch (border)
            {
                case Borders.Top:
                    Rows.Insert(0, Factory.Instance.GetNewRow(ColumnCount));
                    break;
                case Borders.Bottom:
                    Rows.Add(Factory.Instance.GetNewRow(ColumnCount));
                    break;
            }
        }

        public int GetConsecutiveHorizontalCellCount(Borders border)
        {
            if (null == Rows || Rows.Count == 0 || null == Rows[0]) return 0;
            var columnCount = Rows[0].ColumnCount;
            if (columnCount < 3) return 0;
            var lookUpIndex = GetLookUpIndex(border);
            if (lookUpIndex == -1) return 0;
            var count = 0;
            for (var i = 0; i < columnCount - 2; ++i)
            {
                if (this[lookUpIndex, i].IsAlive
                    && this[lookUpIndex, i + 1].IsAlive
                    && this[lookUpIndex, i + 2].IsAlive)
                    count++;
            }
            return count;
        }

        public int GetConsecutiveVerticalCellCount(Borders border)
        {
            if (null == Rows || 0 == Rows.Count) return 0;
            var lookUpIndex = GetLookUpIndex(border);
            if (lookUpIndex == -1) return 0;
            var count = 0;
            var rowCount = Rows.Count;
            for (var i = 0; i < rowCount - 2; ++i)
            {
                if (!ValidColumnCells(i)) continue;
                if (this[i, lookUpIndex].IsAlive
                    && this[i + 1, lookUpIndex].IsAlive
                    && this[i + 2, lookUpIndex].IsAlive)
                    count++;
            }
            return count;
        }

        public bool IsCellReincarnated(int x, int y)
        {
            var numberOfAliveCells = GetAdjacentsAlive(x, y);
            var isAlive = this[x, y].IsAlive;
            return RuleEngine.WillBeAlive(numberOfAliveCells, isAlive);
        }

        public void RejuvenateCells()
        {
            Parallel.For(0, RowCount,
                         x => Parallel.For(0, ColumnCount,
                                           y => { this[x, y] = Factory.Instance.GetNewCell(); }));
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            for (var i = 0; i < RowCount; ++i)
            {
                stringBuilder.Append(Rows[i].ToString());
                if (i < (RowCount - 1))
                    stringBuilder.Append(Constants.NewLine);
            }
            return stringBuilder.ToString();
        }

        private int GetLookUpIndex(Borders border)
        {
            var lookUpIndex = -1;
            switch (border)
            {
                case Borders.Left:
                    lookUpIndex = 0;
                    break;
                case Borders.Right:
                    lookUpIndex = ColumnCount - 1;
                    break;
                case Borders.Top:
                    lookUpIndex = 0;
                    break;
                case Borders.Bottom:
                    lookUpIndex = RowCount - 1;
                    break;
            }
            return lookUpIndex;
        }

        private int GetAdjacentsAlive(int x, int y)
        {
            var numberOfAdjacentsAlive
                = Constants.Deltas.Count(delta => IsAdjacentCellAlive(x, y, delta.DeltaX, delta.DeltaY));
            return numberOfAdjacentsAlive;
        }

        private bool IsAdjacentCellAlive(int row, int column, int rowDelta, int columnDelta)
        {
            var newRow = row + rowDelta;
            var newColumn = column + columnDelta;
            var isNewRowValid = newRow >= 0 && newRow < RowCount;
            var isNewColumnValid = newColumn >= 0 && newColumn < ColumnCount;
            return (isNewRowValid && isNewColumnValid) && (null != this[newRow, newColumn]) &&
                   this[newRow, newColumn].IsAlive;
        }

        private bool IsValid(int x, int y)
        {
            return (x >= 0 && x < RowCount) && (y >= 0 && y < ColumnCount);
        }

        private bool ValidColumnCells(int i)
        {
            return (null != Rows[i] && null != Rows[i + 1] && null != Rows[i + 2]) &&
                   (Rows[i].ColumnCount != 0 && Rows[i + 1].ColumnCount != 0 && Rows[i + 2].ColumnCount != 0);
        }
    }
}