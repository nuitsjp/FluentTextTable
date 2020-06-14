using System.Collections.Generic;

namespace FluentTextTable
{
    public interface IRowSet<TItem>
    {
        IReadOnlyList<IRow<TItem>> Rows { get; }
        int GetColumnWidth(IColumn<TItem> column);
    }
}