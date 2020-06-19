using System.Collections.Generic;

namespace FluentTextTable
{
    public interface IColumnWidths
    {
        IEnumerable<IColumn> Columns { get; }
        int this[IColumn column] { get; }
    }
}