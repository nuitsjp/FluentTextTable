using System.Collections.Generic;

namespace FluentTextTable
{
    public interface IRow
    {
        IReadOnlyDictionary<IColumn, Cell> Cells { get; }
        int Height { get; }
        int GetColumnWidth(IColumn column);
    }
}