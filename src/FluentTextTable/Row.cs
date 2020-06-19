using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentTextTable
{
    public class Row<TItem> : IRow
    {
        private readonly Dictionary<IColumn, Cell> _cells;

        internal Row(IEnumerable<Cell> cells)
        {
            _cells = cells.ToDictionary(x => x.Column);
            Height = _cells.Values.Max(x => x.Height);
        }

        public IReadOnlyDictionary<IColumn, Cell> Cells => _cells;

        public int Height { get; }

        public int GetColumnWidth(IColumn column) => _cells[column].Width;
        
        internal void Write(TextWriter writer, ITableInstance<TItem> tableInstance)
        {
            for (var lineNumber = 0; lineNumber < Height; lineNumber++)
            {
                tableInstance.Borders.Left.Write(writer);

                Cells[tableInstance.Columns[0]].Write(writer, tableInstance.Columns[0], Height, lineNumber, tableInstance.GetColumnWidth(tableInstance.Columns[0]), tableInstance.Padding);

                for (int i = 1; i < tableInstance.Columns.Count; i++)
                {
                    var column = tableInstance.Columns[i];
                    tableInstance.Borders.InsideVertical.Write(writer);
                    Cells[column].Write(writer, column, Height, lineNumber, tableInstance.GetColumnWidth(column), tableInstance.Padding);
                }

                tableInstance.Borders.Right.Write(writer);
                
                writer.WriteLine();
            }
        }

    }
}