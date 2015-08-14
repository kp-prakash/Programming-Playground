using System.Collections.Generic;
using System.Text;
using Shared;

namespace Entities
{
    public class Row : IRow
    {
        public IList<ICell> Cells { get; private set; }

        public int ColumnCount
        {
            get { return (null != Cells) ? Cells.Count : 0; }
        }

        public ICell this[int i]
        {
            get { return (null != Cells && i >= 0 && i < ColumnCount) ? Cells[i] : null; }
            set
            {
                if (null != Cells && i >= 0 && i < ColumnCount)
                    Cells[i] = value;
            }
        }

        public Row(int columns)
        {
            Cells = Factory.Instance.GetNewCells();
            RejuvenateCells(columns);
        }

        public void RejuvenateCells()
        {
            RejuvenateCells(ColumnCount);
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            for (var i = 0; i < ColumnCount; ++i)
            {
                stringBuilder.Append(null != this[i] ? this[i].GetState() : Constants.Dead);
            }
            return stringBuilder.ToString();
        }

        private void RejuvenateCells(int columns)
        {
            Cells.Clear();
            for (var i = 0; i < columns; ++i)
            {
                Cells.Add(Factory.Instance.GetNewCell());
            }
        }
    }
}