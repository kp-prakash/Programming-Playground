using System.Collections.Generic;

namespace Shared
{
    public interface IBoard
    {
        int ColumnCount { get; }
        int RowCount { get; }
        IList<IRow> Rows { get; }
        ICell this[int x, int y] { get; set; }
        void AddColumn(Borders border);
        void AddRow(Borders border);
        int GetConsecutiveHorizontalCellCount(Borders border);
        int GetConsecutiveVerticalCellCount(Borders border);
        bool IsCellReincarnated(int x, int y);
        void RejuvenateCells();
    }
}