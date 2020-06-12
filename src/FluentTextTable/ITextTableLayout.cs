using System.Collections.Generic;

namespace FluentTextTable
{
    public interface ITextTableLayout<TItem>
    {
        Borders Borders { get; }
        IReadOnlyList<Column<TItem>> Columns { get; }
        int GetColumnWidth(Column<TItem> column);
    }
}