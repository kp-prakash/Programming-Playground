using System.Linq;
using Entities;
using NUnit.Framework;
using Shared;

namespace Tests
{
    [TestFixture]
    public class RowTest
    {
        [Test]
        public void TestRowConstruction()
        {
            var row = Factory.Instance.GetNewRow(2);
            var count = row.Cells.Count(cell => cell == null);
            Assert.True(count == 0);
        }

        [Test]
        public void TestColumnCount()
        {
            const int expectedColumns = 2;
            var row = Factory.Instance.GetNewRow(2);
            Assert.True(row.ColumnCount.Equals(expectedColumns));
        }

        [Test]
        public void TestToString()
        {
            const string expectedOutput = "X-";
            var row = Factory.Instance.GetNewRow(2);
            row[0].Toggle();
            Assert.True(expectedOutput.Equals(row.ToString()));
        }

        [Test]
        public void TestRejuvenateCells()
        {
            var row = Factory.Instance.GetNewRow(2);
            foreach (var cell in row.Cells)
            {
                cell.Toggle();
            }
            var count = GetAliveCellCount(row);
            row.RejuvenateCells();
            var newCount = GetAliveCellCount(row);
            Assert.True(count == 2 && newCount == 0);
        }

        private int GetAliveCellCount(IRow row)
        {
            return row.Cells.Count(cell => cell.IsAlive);
        }
    }
}