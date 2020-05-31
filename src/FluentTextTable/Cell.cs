using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentTextTable
{
    internal class Cell
    {

        private readonly CellLine[] _cellLines;
        public Cell(object value, string format)
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

        internal int Width { get; }
        internal int Height => _cellLines.Length;

        private IEnumerable<object> Split(string value)
        {
            using (var reader = new StringReader(value))
            {
                for (var line = reader.ReadLine(); line != null; line = reader.ReadLine())
                {
                    yield return line;
                }
            }
        }

        internal void WritePlanText(
            TextWriter writer,
            Row row,
            Column column,
            int lineNumber)
        {
            CellLine value;
            switch (column.VerticalAlignment)
            {
                case VerticalAlignment.Top:
                    value = GetTopCellLine();
                    break;
                case VerticalAlignment.Center:
                    value = GetCenterCellLine();
                    break;
                case VerticalAlignment.Bottom:
                    value = GetBottomCellLine();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            value.WritePlanText(writer, column);

            CellLine GetTopCellLine()
            {
                return lineNumber < Height
                    ? _cellLines[lineNumber]
                    : CellLine.Empty;
            }

            CellLine GetCenterCellLine()
            {
                var indent = (row.Height - Height) / 2;
                var localLineNumber = lineNumber - indent;
                if (localLineNumber < 0)
                {
                    return CellLine.Empty;
                }

                if (Height <= localLineNumber)
                {
                    return CellLine.Empty;
                }

                return _cellLines[localLineNumber];
            }

            CellLine GetBottomCellLine()
            {
                var indent = row.Height - Height;
                var localLineNumber = lineNumber - indent;
                if (localLineNumber < 0)
                {
                    return CellLine.Empty;
                }

                return _cellLines[localLineNumber];
            }
        }
        
        internal void WriteMarkdown(
            TextWriter writer,
            Column column)
        {
            writer.Write(' ');
            if (_cellLines.Length == 1)
            {
                // In the case of 1line, padding should match the width of the column.
                _cellLines
                    .Single()
                    .WriteMarkdown(writer, column);
            }
            else
            {
                // If you're multi-line in Markdown, you can't match the widths, so it doesn't padding.
                // Simply combine them with <br> to describe them.
                writer.Write(string.Join("<br>", _cellLines.Select(x => x.Value)));
            }
            writer.Write(" |");
        }
    }
}