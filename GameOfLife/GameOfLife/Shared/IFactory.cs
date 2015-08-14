using System.Collections.Generic;

namespace Shared
{
    public interface IFactory
    {
        IBoard GetNewBoard(int rowCount, int columnCount);
        ICell GetNewCell();
        IList<ICell> GetNewCells();
        IGameOfLife GetNewGameOfLife(int rowCount, int columnCount);
        IRow GetNewRow(int columns);
        IList<IRow> GetNewRows(int rowCount, int columnCount);
    }
}