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


        public int GetColumnWidth(IColumn column) => _cells[column].Width;
        
        public void Write(TextWriter writer, ITableLayout tableLayout)
        {
            for (var lineNumber = 0; lineNumber < _height; lineNumber++)
            {
                tableLayout.Borders.Left.Write(writer);

                _cells[tableLayout.Columns[0]].Write(writer, _height, lineNumber, tableLayout.GetColumnWidth(tableLayout.Columns[0]), tableLayout.Padding);

                for (int i = 1; i < tableLayout.Columns.Count; i++)
                {
                    var column = tableLayout.Columns[i];
                    tableLayout.Borders.InsideVertical.Write(writer);
                    _cells[column].Write(writer, _height, lineNumber, tableLayout.GetColumnWidth(column), tableLayout.Padding);
                }

                tableLayout.Borders.Right.Write(writer);
                
                writer.WriteLine();
            }
        }

    }
}