using System;
using System.Linq;
using System.Threading;
using Shared;
using Shared.Properties;
using Utils;

namespace Entities
{
    public class InputParser : IInputParser
    {
        public IGameOfLife CurrentGame { get; private set; }

        private InputParser()
        {
        }

        public static IInputParser Instance
        {
            get { return Singleton<InputParser>.Instance; }
        }
    
        public void InitializeGame(string input, Action<int,IBoard> notificationCallBack = null)
        {
            ValidateInput(input);
            var rows = input.Split(Constants.NewLine);
            ValidateRows(rows);
            var rowCount = rows.Length;
            var columnCount = rows[0].Length;
            CurrentGame = Factory.Instance.GetNewGameOfLife(rowCount, columnCount);
            CurrentGame.OnGenerationCompleted += notificationCallBack;
            InitializeGame(rows, rowCount, columnCount);
        }

        public void StartGame()
        {
            var count = 0;
            while (count < int.MaxValue)
            {
                CurrentGame.ProcessGeneration();
                Thread.Sleep(100);
                count++;
            }
            CurrentGame = null;
        }

        public void ProcessFirstGeneration()
        {
            if (null == CurrentGame)
                throw new Exception(Resources.GameOver);
            CurrentGame.ProcessGeneration();
        }

        private void InitializeGame(string[] rows, int rowCount,int columnCount)
        {
            var board = CurrentGame.CurrentGeneration;
            for (var i = 0; i < rowCount; ++i)
            {
                for (var j = 0; j < columnCount; j++)
                {
                    if (Constants.Alive.Equals(rows[i][j]))
                        board[i, j].Toggle();
                }
            }
        }

        private void ValidateInput(string input)
        {
            if (null == input) throw new ArgumentNullException(Resources.InputNull);
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException(Resources.InvalidInputString);
        }

        private void ValidateRows(string[] rows)
        {
            if (null == rows) throw new ArgumentNullException(Resources.RowsNull);
            if (rows.Length == 0 || rows.Count(row => row.Length == 0) > 0)
                throw new FormatException(Resources.InvalidInputString);
        }
    }
}