using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentTextTable
{
    public class Row<TItem>
    {
        private readonly Borders _borders;

        private readonly Dictionary<Column<TItem>, Cell> _cells = new Dictionary<Column<TItem>, Cell>();

        internal Row(IList<Column<TItem>> columns, Borders borders, TItem item)
        {
            _borders = borders;

            foreach (var column in columns)
            {
                _cells[column] = new Cell(column, column.GetValue(item));
            }

            Height = _cells.Values.Max(x => x.Height);
        }

        public Dictionary<Column<TItem>, Cell> Cells => _cells;

        public int Height { get; }

        public int GetColumnWidth(Column<TItem> column) => _cells[column].Width;
    }
}