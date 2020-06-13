using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentTextTable
{
    public class Row<TItem>
    {
        private readonly Dictionary<IColumn<TItem>, Cell> _cells = new Dictionary<IColumn<TItem>, Cell>();

        internal Row(IEnumerable<IColumn<TItem>> columns, TItem item)
        {
            foreach (var column in columns)
            {
                _cells[column] = new Cell(column, column.GetValue(item));
            }

            Height = _cells.Values.Max(x => x.Height);
        }

        public Dictionary<IColumn<TItem>, Cell> Cells => _cells;

        public int Height { get; }

        public int GetColumnWidth(Column<TItem> column) => _cells[column].Width;
    }
}