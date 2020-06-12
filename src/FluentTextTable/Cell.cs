using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentTextTable
{
    public class Cell<TItem>
    {
        private readonly CellLine<TItem>[] _cellLines;
       
        internal Cell(Column<TItem> column, TItem item)
        {
            var value = column.GetValue(item);
            IEnumerable<object> values;
            if (value is string stringValue)
            {
                values = Split(stringValue);
            }
            else if(value is IEnumerable<object> enumerable)
            {
                values = enumerable;
            }
            else
            {
                values = new[] {value};
            }

            _cellLines = values.Select(x => new CellLine<TItem>(column, x)).ToArray();

            Width = _cellLines.Max(x =>x.Width);
        }

        public int Width { get; }
        public int Height => _cellLines.Length;

        public CellLine<TItem> GetCellLine(int lineNumber) => _cellLines[lineNumber];

        public IEnumerable<CellLine<TItem>> GetCellLines() => _cellLines;

        private IEnumerable<object> Split(string value)
        {
            using var reader = new StringReader(value);
            for (var line = reader.ReadLine(); line != null; line = reader.ReadLine())
            {
                yield return line;
            }
        }
   }
}