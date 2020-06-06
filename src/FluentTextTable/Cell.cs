using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentTextTable
{
    internal class Cell<TItem>
    {

        private readonly Column<TItem> _column;
        private readonly CellLine<TItem> _blankCellLine;
        private readonly CellLine<TItem>[] _cellLines;
       
        internal Cell(Column<TItem> column, TItem item)
        {
            _column = column;
            _blankCellLine = new CellLine<TItem>(column);
            
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
            TextWriter textWriter,
            ITextTable<TItem> table,
            int rowHeight,
            int lineNumber)
        {
            CellLine<TItem> value;
            switch (_column.VerticalAlignment)
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

            value.WritePlanText(textWriter, table);

            CellLine<TItem> GetTopCellLine()
            {
                return lineNumber < Height
                    ? _cellLines[lineNumber]
                    : _blankCellLine;
            }

            CellLine<TItem> GetCenterCellLine()
            {
                var indent = (rowHeight - Height) / 2;
                var localLineNumber = lineNumber - indent;
                if (localLineNumber < 0)
                {
                    return _blankCellLine;
                }

                if (Height <= localLineNumber)
                {
                    return _blankCellLine;
                }

                return _cellLines[localLineNumber];
            }

            CellLine<TItem> GetBottomCellLine()
            {
                var indent = rowHeight - Height;
                var localLineNumber = lineNumber - indent;
                if (localLineNumber < 0)
                {
                    return _blankCellLine;
                }

                return _cellLines[localLineNumber];
            }
        }
        
        internal void WriteMarkdown(
            TextWriter textWriter,
            ITextTable<TItem> table,
            Column<TItem> column)
        {
            textWriter.Write(' ');
            if (_cellLines.Length == 1)
            {
                // In the case of 1line, padding should match the width of the column.
                _cellLines
                    .Single()
                    .WriteMarkdown(textWriter, table);
            }
            else
            {
                // If you're multi-line in Markdown, you can't match the widths, so it doesn't padding.
                // Simply combine them with <br> to describe them.
                textWriter.Write(string.Join("<br>", _cellLines.Select(x => x.Value)));
            }
            textWriter.Write(" |");
        }
    }
}