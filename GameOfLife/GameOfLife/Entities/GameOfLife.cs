using System;
using System.Threading.Tasks;
using Shared;
using Shared.Properties;

namespace Entities
{
    public class GameOfLife : IGameOfLife
    {
        public int RowCount
        {
            get { return (null != CurrentGeneration) ? CurrentGeneration.RowCount : 0; }
        }

        public int ColumnCount
        {
            get { return (null != CurrentGeneration) ? CurrentGeneration.ColumnCount : 0; }
        }

        public event Action<int, IBoard> OnGenerationCompleted;

        public IBoard CurrentGeneration { get; private set; }

        public IBoard NextGeneration { get; private set; }

        public int Generation { get; private set; }

        public GameOfLife(int rowCount, int columnCount)
        {
            if (rowCount <= 0 && columnCount <= 0)
                throw new ArgumentException(Resources.InvalidBoardSize);
            InitializeGenerations(rowCount, columnCount);
        }

        public void ProcessGeneration()
        {
            if (Generation != 0)
            {
                UpdateGeneration();
                NextGeneration.RejuvenateCells();
            }
            ProcessCurrentGeneration();
        }

        private void InitializeGenerations(int rowCount, int columnCount)
        {
            CurrentGeneration = Factory.Instance.GetNewBoard(rowCount, columnCount);
            NextGeneration = Factory.Instance.GetNewBoard(rowCount, columnCount);
        }

        private void ProcessCurrentGeneration()
        {
            CheckBorderGrowth();
            Parallel.For(0, RowCount,
                         x => Parallel.For(0, ColumnCount,
                                           y => ToggleCellInNextGeneration(x, y)));
            Generation++;
            NotifyObservers();
        }

        private void CheckBorderGrowth()
        {
            CheckColumnGrowth(Borders.Left);
            CheckColumnGrowth(Borders.Right);
            CheckRowGrowth(Borders.Top);
            CheckRowGrowth(Borders.Bottom);
        }

        private void CheckColumnGrowth(Borders border)
        {
            var aliveCellGroupCount 
                = CurrentGeneration.GetConsecutiveVerticalCellCount(border);
            if (aliveCellGroupCount >= 1)
            {
                AddColumn(border);
            }
        }

        private void CheckRowGrowth(Borders border)
        {
            var aliveCellGroupCount 
                = CurrentGeneration.GetConsecutiveHorizontalCellCount(border);
            if (aliveCellGroupCount >= 1)
            {
                AddRow(border);
            }
        }

        private void AddColumn(Borders border)
        {
            CurrentGeneration.AddColumn(border);
            NextGeneration.AddColumn(border);
        }

        private void AddRow(Borders border)
        {
            CurrentGeneration.AddRow(border);
            NextGeneration.AddRow(border);
        }

        

        private void NotifyObservers()
        {
            if (null != OnGenerationCompleted)
                OnGenerationCompleted(Generation, NextGeneration);
        }

        private void UpdateGeneration()
        {
            Parallel.For(0, RowCount,
                         x => Parallel.For(0, ColumnCount,
                                           y => { CurrentGeneration[x, y] = NextGeneration[x, y]; }));
        }

        private void ToggleCellInNextGeneration(int x, int y)
        {
            var isAliveInNextGen = CurrentGeneration.IsCellReincarnated(x, y);
            if (isAliveInNextGen) NextGeneration[x, y].Toggle();
        }
    }
}