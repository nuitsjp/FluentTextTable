using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentTextTable
{
    internal class Cell<TItem>
    {

        private readonly Column<TItem> _column;
        private readonly CellLine<TItem>[] _cellLines;
       
        internal Cell(Column<TItem> column, TItem item)
        {
            _column = column;
            
            var value = _column.GetValue(item);
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

            _cellLines = values.Select(x => new CellLine<TItem>(_column, x)).ToArray();

            Width = _cellLines.Max(x =>x.Width);
        }

        internal int Width { get; }
        internal int Height => _cellLines.Length;

        internal CellLine<TItem> GetCellLine(int lineNumber) => _cellLines[lineNumber];

        internal IEnumerable<CellLine<TItem>> GetCellLines() => _cellLines;

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