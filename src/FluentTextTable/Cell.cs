using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentTextTable
{
    public class Cell
    {
        private readonly CellLine[] _cellLines;
       
        internal Cell(object value, string format)
        {
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

            _cellLines = values.Select(x => new CellLine(x, format)).ToArray();

            Width = _cellLines.Max(x =>x.Width);
        }

        public int Width { get; }
        public int Height => _cellLines.Length;

        public CellLine GetCellLine(int lineNumber) => _cellLines[lineNumber];

        public IEnumerable<CellLine> GetCellLines() => _cellLines;

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