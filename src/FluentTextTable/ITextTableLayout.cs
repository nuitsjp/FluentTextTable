using System.Collections.Generic;

namespace FluentTextTable
{
    public interface ITextTableLayout
    {
        IBorders Borders { get; }
        IMargins Margins { get; }
        IPaddings Paddings { get; }
#if NET40
        IList<IColumn> Columns { get; }
#else
        IReadOnlyList<IColumn> Columns { get; }
#endif
        int GetColumnWidth(IColumn column);
    }
}