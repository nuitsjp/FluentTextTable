using System.Collections.Generic;

namespace FluentTextTable
{
    internal interface ITextTableLayout<TItem>
    {
        Borders Borders { get; }
        IReadOnlyList<Column<TItem>> Columns { get; }
        int GetColumnWidth(Column<TItem> column);
        IReadOnlyDictionary<Column<TItem>, CellLine<TItem>> BlankCellLines { get; }
    }
}