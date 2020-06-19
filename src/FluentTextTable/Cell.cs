using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentTextTable
{
    public class Cell
    {
        private readonly CellLine[] _cellLines;
       
        internal Cell(IColumn column, IEnumerable<CellLine> cellLines)
        {
            Column = column;
            _cellLines = cellLines.ToArray();
            Width = _cellLines.Max(x =>x.Width);
        }

        public IColumn Column { get; }
        public int Width { get; }
        public int Height => _cellLines.Length;

        internal void Write(
            TextWriter writer,
            IColumn column,
            int rowHeight,
            int lineNumber,
            int columnWidth,
            int padding)
        {
            CellLine cellLine;
            switch (column.VerticalAlignment)
            {
                case VerticalAlignment.Top:
                    cellLine = GetTopCellLine();
                    break;
                case VerticalAlignment.Center:
                    cellLine = GetCenterCellLine();
                    break;
                case VerticalAlignment.Bottom:
                    cellLine = GetBottomCellLine();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            cellLine.Write(writer, column, columnWidth, padding);

            CellLine GetTopCellLine()
            {
                return lineNumber < Height
                    ? _cellLines[lineNumber]
                    : CellLine.BlankCellLine;
            }

            CellLine GetCenterCellLine()
            {
                var indent = (rowHeight - Height) / 2;
                var localLineNumber = lineNumber - indent;
                if (localLineNumber < 0)
                {
                    return CellLine.BlankCellLine;
                }

                if (Height <= localLineNumber)
                {
                    return CellLine.BlankCellLine;
                }

                return _cellLines[localLineNumber];
            }

            CellLine GetBottomCellLine()
            {
                var indent = rowHeight - Height;
                var localLineNumber = lineNumber - indent;
                if (localLineNumber < 0)
                {
                    return CellLine.BlankCellLine;
                }

                return _cellLines[localLineNumber];
            }
        }
    }
}