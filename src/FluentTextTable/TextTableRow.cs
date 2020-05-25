using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace FluentTextTable
{
    internal class TextTableRow<TItem>
    {
        private readonly Dictionary<ITextTableColumn, TextTableCell<TItem>> _cells = new Dictionary<ITextTableColumn, TextTableCell<TItem>>();

        internal IReadOnlyDictionary<ITextTableColumn, TextTableCell<TItem>> Cells => _cells;

        internal void AddCell(TextTableCell<TItem> cell)
        {
            _cells[cell.Column] = cell;
        }
    }
}