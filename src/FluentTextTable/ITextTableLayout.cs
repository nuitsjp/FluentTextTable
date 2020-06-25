using System.Collections.Generic;

namespace FluentTextTable
{
    public interface ITextTableLayout
    {
        IBorders Borders { get; }
        IMargins Margins { get; }
        int Padding { get; }
        IReadOnlyList<IColumn> Columns { get; }
        int GetColumnWidth(IColumn column);
    }
}