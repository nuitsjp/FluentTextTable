using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace FluentTextTable
{
    internal class TextTableRow
    {
        private readonly Dictionary<ITextTableColumn, TextTableCell> _cells = new Dictionary<ITextTableColumn, TextTableCell>();

        internal IReadOnlyDictionary<ITextTableColumn, TextTableCell> Cells => _cells;

        internal void AddCell(TextTableCell cell)
        {
            _cells[cell.Column] = cell;
        }
    }
}