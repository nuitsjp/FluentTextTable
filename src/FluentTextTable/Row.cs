using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentTextTable
{
    public class Row : IRow
    {
        private readonly int _height;

        private readonly IReadOnlyDictionary<IColumn, Cell> _cells;

        internal Row(IEnumerable<Cell> cells)
        {
            _cells = cells.ToDictionary(x => x.Column);
            _height = _cells.Values.Max(x => x.Height);
        }


        public int GetWidthOf(IColumn column) => _cells[column].Width;
        
        public void Write(TextWriter writer, ITextTableLayout textTableLayout)
        {
            for (var lineNumber = 0; lineNumber < _height; lineNumber++)
            {
                textTableLayout.Borders.Left.Write(writer);

                _cells[textTableLayout.Columns[0]].Write(writer, _height, lineNumber, textTableLayout.GetWidthOf(textTableLayout.Columns[0]), textTableLayout.Padding);

                for (var i = 1; i < textTableLayout.Columns.Count; i++)
                {
                    var column = textTableLayout.Columns[i];
                    textTableLayout.Borders.InsideVertical.Write(writer);
                    _cells[column].Write(writer, _height, lineNumber, textTableLayout.GetWidthOf(column), textTableLayout.Padding);
                }

                textTableLayout.Borders.Right.Write(writer);
                
                writer.WriteLine();
            }
        }

    }
}