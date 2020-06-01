using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentTextTable
{
    internal class Row<TItem>
    {
        private readonly Dictionary<Column<TItem>, Cell<TItem>> _cells;
        internal int Height { get; }

        internal static Row<TItem> Create(TItem item, IList<Column<TItem>> columns)
        {
            var cells = new Dictionary<Column<TItem>, Cell<TItem>>();
            foreach (var column in columns)
            {
                cells[column] = new Cell<TItem>(column.GetValue(item), column.Format);
            }

            var height = cells.Values.Max(x => x.Height);

            return new Row<TItem>(cells, height);
        }


        private Row(Dictionary<Column<TItem>, Cell<TItem>> cells, int height)
        {
            _cells = cells;
            Height = height;
        }

        internal int GetWidth(Column<TItem> column) => _cells[column].Width;

        internal void WritePlanText(TextTableWriter<TItem> writer, IList<Column<TItem>> columns, Borders borders)
        {
            // Write line in row.
            for (var lineNumber = 0; lineNumber < Height; lineNumber++)
            {
                borders.Left.Write(writer);

                _cells[columns.First()].WritePlanText(writer, this, columns.First(), lineNumber);
                
                foreach (var column in columns.Skip(1))
                {
                    borders.InsideVertical.Write(writer);
                    _cells[column].WritePlanText(writer, this, column, lineNumber);
                }

                borders.Right.Write(writer);
                
                writer.WriteLine();
            }
        }
        
        internal void WriteMarkdown(TextTableWriter<TItem> writer, IList<Column<TItem>> columns)
        {
            writer.Write("|");
            foreach (var column in columns)
            {
                _cells[column].WriteMarkdown(writer, column);
            }
            writer.WriteLine();
        }
    }
}