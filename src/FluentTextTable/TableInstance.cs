using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentTextTable
{
    public abstract class TableInstance<TItem> : ITableInstance<TItem>
    {
        private readonly Table<TItem> _table;
        private readonly Dictionary<IColumn<TItem>, int> _columnWidths = new Dictionary<IColumn<TItem>, int>();

        private readonly List<IRow<TItem>> _rows;

        internal TableInstance(Table<TItem> table, List<IRow<TItem>> rows)
        {
            _rows = rows;
            _table = table;
            foreach (var column in Columns)
            {
                _columnWidths[column] = 
                    Math.Max(
                        column.HeaderWidth, 
                        rows.Max(x => x.GetColumnWidth(column)))
                    + Padding * 2;
            }
        }

        public IReadOnlyList<IColumn<TItem>> Columns => _table.Columns;
        public int Padding => _table.Padding;
        public IReadOnlyList<IRow<TItem>> Rows => _rows;
        public int GetColumnWidth(IColumn<TItem> column) => _columnWidths[column];
    }
}