using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentTextTable
{
    internal class Row<TItem>
    {
        private readonly IList<Column<TItem>> _columns;

        private readonly Borders _borders;

        private readonly Dictionary<Column<TItem>, Cell<TItem>> _cells = new Dictionary<Column<TItem>, Cell<TItem>>();

        internal Row(IList<Column<TItem>> columns, Borders borders, TItem item)
        {
            _columns = columns;
            _borders = borders;

            foreach (var column in _columns)
            {
                _cells[column] = new Cell<TItem>(column, item);
            }

            Height = _cells.Values.Max(x => x.Height);
        }

        public Dictionary<Column<TItem>, Cell<TItem>> Cells => _cells;

        internal int Height { get; }

        internal int GetColumnWidth(Column<TItem> column) => _cells[column].Width;
    }
}