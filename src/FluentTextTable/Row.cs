using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace FluentTextTable
{
    internal class Row
    {
        private readonly Dictionary<IColumn, Cell> _cells;
        internal int Height { get; }

        public Row(Dictionary<IColumn, Cell> cells, int height)
        {
            _cells = cells;
            Height = height;
        }

        internal static Row Create<TItem>(TItem item, IReadOnlyDictionary<Column, MemberAccessor<TItem>> memberAccessors)
        {
            var cells = new Dictionary<IColumn, Cell>();
            foreach (var keyValue in memberAccessors)
            {
                cells[keyValue.Key] = new Cell(keyValue.Value.GetValue(item), keyValue.Key.Format);
            }

            var height = cells.Values.Max(x => x.Height);

            return new Row(cells, height);
        }

        internal Cell GetCell(Column column) => _cells[column];

        internal void WritePlanText(TextWriter writer, IList<Column> columns)
        {
            // Write line in row.
            for (var lineNumber = 0; lineNumber < Height; lineNumber++)
            {
                writer.Write("|");
                foreach (var column in columns)
                {
                    _cells[column].WritePlanText(writer, this, column, lineNumber);
                }
                writer.WriteLine();
            }
        }
        
        internal void WriteMarkdown(TextWriter writer, IList<Column> columns)
        {
            writer.Write("|");
            foreach (var column in columns)
            {
                _cells[column].WriteMarkdown(writer, this, column);
            }
            writer.WriteLine();
        }
    }
}