using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace FluentTextTable
{
    internal class Row<TItem>
    {
        private readonly Dictionary<IColumn, Cell<TItem>> _cells = new Dictionary<IColumn, Cell<TItem>>();

        public Row(TItem item, IEnumerable<Column<TItem>> columns)
        {
            foreach (var column in columns)
            {
                _cells[column] = column.ToCell(item);
            }

            Height = Cells.Values.Max(x => x.Height);
        }

        internal IReadOnlyDictionary<IColumn, Cell<TItem>> Cells => _cells;

        internal int Height { get; }
    }
}