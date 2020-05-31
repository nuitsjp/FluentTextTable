using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentTextTable
{
    internal class Row
    {
        private readonly Dictionary<IColumnConfig, Cell> _cells;
        internal int Height { get; }

        private Row(Dictionary<IColumnConfig, Cell> cells, int height)
        {
            _cells = cells;
            Height = height;
        }

        internal static Row Create<TItem>(TItem item, IReadOnlyDictionary<ColumnConfig, MemberAccessor<TItem>> memberAccessors)
        {
            var cells = new Dictionary<IColumnConfig, Cell>();
            foreach (var keyValue in memberAccessors)
            {
                cells[keyValue.Key] = new Cell(keyValue.Value.GetValue(item), keyValue.Key.Format);
            }

            var height = cells.Values.Max(x => x.Height);

            return new Row(cells, height);
        }

        internal Cell GetCell(ColumnConfig columnConfig) => _cells[columnConfig];

        internal void WritePlanText(TextWriter writer, IList<ColumnConfig> columns, Borders borders)
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
        
        internal void WriteMarkdown(TextWriter writer, IList<ColumnConfig> columns)
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