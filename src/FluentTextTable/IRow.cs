using System.Collections.Generic;

namespace FluentTextTable
{
    public interface IRow<TItem>
    {
        IReadOnlyDictionary<IColumn<TItem>, Cell> Cells { get; }
        int Height { get; }
        int GetColumnWidth(IColumn<TItem> column);
    }
}