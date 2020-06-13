using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentTextTable
{
    public class RowSet<TItem> : IRowSet<TItem>
    {
        private readonly Dictionary<Column<TItem>, int> _columnWidths = new Dictionary<Column<TItem>, int>();

        private readonly List<Row<TItem>> _rows;

        internal RowSet(IEnumerable<Column<TItem>> columns, List<Row<TItem>> rows)
        {
            _rows = rows;
            foreach (var column in columns)
            {
                _columnWidths[column] = 
                    Math.Max(
                        column.HeaderWidth, 
                        rows.Max(x => x.GetColumnWidth(column)));
            }
        }

        public IReadOnlyList<Row<TItem>> Rows => _rows;
        public int GetColumnWidth(Column<TItem> column) => _columnWidths[column];
    }
}