using System.Collections.Generic;

namespace FluentTextTable
{
    public interface ITextTableLayout
    {
        IBorders Borders { get; }
        IMargins Margins { get; }
        IPaddings Paddings { get; }
        IReadOnlyList<IColumn> Columns { get; }
        int GetColumnWidth(IColumn column);
    }
}