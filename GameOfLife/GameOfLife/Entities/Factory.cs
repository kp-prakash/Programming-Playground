using System.Collections.Generic;
using System.Threading.Tasks;
using Shared;
using Utils;

namespace Entities
{
    public class Factory : IFactory
    {
        private Factory()
        {

        }

        public static IFactory Instance
        {
            get { return Singleton<Factory>.Instance; }
        }

        public IBoard GetNewBoard(int rowCount, int columnCount)
        {
            return new Board(rowCount, columnCount);
        }

        public ICell GetNewCell()
        {
            return new Cell();
        }

        public IList<ICell> GetNewCells()
        {
            return new List<ICell>();
        }

        public IGameOfLife GetNewGameOfLife(int rowCount, int columnCount)
        {
            return new GameOfLife(rowCount, columnCount);
        }

        public IRow GetNewRow(int columns)
        {
            return new Row(columns);
        }
        
        public IList<IRow> GetNewRows(int rowCount, int columnCount)
        {
            var rows = new List<IRow>();
            Parallel.For(0, rowCount, i => rows.Add(GetNewRow(columnCount)));
            return rows;
        }
    }
}