using System.Collections.Generic;

namespace FluentTextTable
{
    public interface IRowSet<TItem>
    {
        IReadOnlyList<Row<TItem>> Rows { get; }
        int GetColumnWidth(Column<TItem> column);
    }
}