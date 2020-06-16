using System.Collections.Generic;

namespace FluentTextTable
{
    public interface ITableInstance<TItem>
    {
        int Padding { get; }
        IReadOnlyList<IColumn<TItem>> Columns { get; }
        IReadOnlyList<IRow<TItem>> Rows { get; }
        int GetColumnWidth(IColumn<TItem> column);
    }
}