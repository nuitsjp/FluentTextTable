using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentTextTable
{
    public class Cell : ICell
    {
        private readonly IColumn _column;
        private readonly ICellLine[] _cellLines;
       
        internal Cell(IColumn column, IEnumerable<ICellLine> cellLines)
        {
            _column = column;
            _cellLines = cellLines.ToArray();
            Width = _cellLines.Max(x =>x.Width);
        }

        public int Width { get; }
        public int Height => _cellLines.Length;

        public void Write(
            TextWriter textWriter,
            int rowHeight,
            int lineNumber,
            ITextTableLayout textTableLayout)
        {
            ICellLine cellLine;
            switch (_column.VerticalAlignment)
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

            cellLine.Write(textWriter, textTableLayout, _column);

            ICellLine GetTopCellLine()
            {
                return lineNumber < Height
                    ? _cellLines[lineNumber]
                    : CellLine.BlankCellLine;
            }

            ICellLine GetCenterCellLine()
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

            ICellLine GetBottomCellLine()
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