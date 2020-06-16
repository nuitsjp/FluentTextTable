using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentTextTable
{
    public class Row<TItem> : IRow<TItem>
    {
        private readonly int _padding;
        private readonly Dictionary<IColumn<TItem>, Cell> _cells = new Dictionary<IColumn<TItem>, Cell>();

        internal Row(IEnumerable<IColumn<TItem>> columns, int padding, TItem item)
        {
            _padding = padding;
            foreach (var column in columns)
            {
                _cells[column] = new Cell(column.GetValue(item), column.Format);
            }

            Height = _cells.Values.Max(x => x.Height);
        }

        public IReadOnlyDictionary<IColumn<TItem>, Cell> Cells => _cells;

        public int Height { get; }

        public int GetColumnWidth(IColumn<TItem> column) => _cells[column].Width;
    }
}