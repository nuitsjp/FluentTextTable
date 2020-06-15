using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentTextTable
{
    public class RowSet<TItem> : IRowSet<TItem>
    {
        private readonly Dictionary<IColumn<TItem>, int> _columnWidths = new Dictionary<IColumn<TItem>, int>();

        private readonly List<IRow<TItem>> _rows;

        internal RowSet(ITable<TItem> table, List<IRow<TItem>> rows)
        {
            _rows = rows;
            foreach (var column in table.Columns)
            {
                _columnWidths[column] = 
                    Math.Max(
                        column.HeaderWidth, 
                        rows.Max(x => x.GetColumnWidth(column)))
                    + table.Padding * 2;
            }
        }

        public IReadOnlyList<IRow<TItem>> Rows => _rows;
        public int GetColumnWidth(IColumn<TItem> column) => _columnWidths[column];
    }
}