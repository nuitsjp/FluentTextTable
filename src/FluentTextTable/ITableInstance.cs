using System.Collections.Generic;

namespace FluentTextTable
{
    public interface ITableInstance<TItem> : ITable<TItem>
    {
        int Padding { get; }
        Borders Borders { get; }
        IReadOnlyList<IColumn> Columns { get; }
        int GetColumnWidth(IColumn column);
    }
}