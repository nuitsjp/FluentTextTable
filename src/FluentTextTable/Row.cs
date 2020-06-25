using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentTextTable
{
    public class Row : IRow
    {
        private readonly int _height;

        private readonly IReadOnlyDictionary<IColumn, ICell> _cells;

        internal Row(IReadOnlyDictionary<IColumn, ICell> cells)
        {
            _cells = cells;
            _height = _cells.Values.Max(x => x.Height);
        }


        public int GetCellWidth(IColumn column) => _cells[column].Width;
        
        public void Write(TextWriter textWriter, ITextTableLayout textTableLayout)
        {
            for (var lineNumber = 0; lineNumber < _height; lineNumber++)
            {
                textTableLayout.Margins.Left.Write(textWriter);
                textTableLayout.Borders.Left.Write(textWriter);

                _cells[textTableLayout.Columns[0]].Write(textWriter, _height, lineNumber, textTableLayout.GetColumnWidth(textTableLayout.Columns[0]), textTableLayout.Padding);

                for (var i = 1; i < textTableLayout.Columns.Count; i++)
                {
                    var column = textTableLayout.Columns[i];
                    textTableLayout.Borders.InsideVertical.Write(textWriter);
                    _cells[column].Write(textWriter, _height, lineNumber, textTableLayout.GetColumnWidth(column), textTableLayout.Padding);
                }

                textTableLayout.Borders.Right.Write(textWriter);
                textTableLayout.Margins.Right.Write(textWriter);
                
                textWriter.WriteLine();
            }
        }

    }
}