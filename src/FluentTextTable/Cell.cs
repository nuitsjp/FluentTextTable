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
            int rowHeight,
            int lineNumber,
            int columnWidth,
            int padding)
        {
            var cellLine = Column.VerticalAlignment switch
            {
                VerticalAlignment.Top => GetTopCellLine(),
                VerticalAlignment.Center => GetCenterCellLine(),
                VerticalAlignment.Bottom => GetBottomCellLine(),
                _ => throw new ArgumentOutOfRangeException()
            };

            cellLine.Write(writer, Column, columnWidth, padding);

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

                return Height <= localLineNumber 
                    ? CellLine.BlankCellLine 
                    : _cellLines[localLineNumber];
            }

            CellLine GetBottomCellLine()
            {
                var indent = rowHeight - Height;
                var localLineNumber = lineNumber - indent;
                return localLineNumber < 0 
                    ? CellLine.BlankCellLine 
                    : _cellLines[localLineNumber];
            }
        }
    }
}