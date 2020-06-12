using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentTextTable
{
    internal class TextTableLayout<TItem> : ITextTableLayout<TItem>
    {
        private readonly Dictionary<Column<TItem>, int> _columnWidths = new Dictionary<Column<TItem>, int>();

        internal TextTableLayout(Borders borders, IEnumerable<Column<TItem>> columns, IEnumerable<Row<TItem>> rows)
        {
            Borders = borders;
            Columns = columns.ToList();
            foreach (var column in Columns)
            {
                _columnWidths[column] = 
                    Math.Max(
                        column.HeaderWidth, 
                        rows.Max(x => x.GetColumnWidth(column)));
            }
        }

        public Borders Borders { get; }
        public IReadOnlyList<Column<TItem>> Columns { get; }
        public int GetColumnWidth(Column<TItem> column) => _columnWidths[column];
    }
}