using System.Collections.Generic;

namespace FluentTextTable
{
    public interface ITextTableLayout
    {
        Borders Borders { get; }
        int Padding { get; }
        IReadOnlyList<IColumn> Columns { get; }
        int GetColumnWidth(IColumn column);
    }
}