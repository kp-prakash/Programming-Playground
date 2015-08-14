using System.Collections.Generic;

namespace Shared
{
    public interface IRow
    {
        ICell this[int i] { get; set; }
        IList<ICell> Cells { get; }
        int ColumnCount { get; }
        void RejuvenateCells();
    }
}