using System;
using System.Collections.Generic;

namespace FluentTextTable
{
    public class TextTableLayout : ITextTableLayout
    {
        private readonly Dictionary<IColumn, int> _columnWidths = new Dictionary<IColumn, int>();

#if NET40
        internal TextTableLayout(IList<IColumn> columns, IBorders borders, IMargins margins, IPaddings paddings, IRowSet rowSet)
#else
        internal TextTableLayout(IReadOnlyList<IColumn> columns, IBorders borders, IMargins margins, IPaddings paddings, IRowSet rowSet)
#endif
        {
            Columns = columns;
            Borders = borders;
            Margins = margins;
            Paddings = paddings;
            foreach (var column in Columns)
            {
                var maxColumnWidth =
                    Math.Max(
                        column.Width,
                        rowSet.GetMaxCellWidth(column))
                    + Paddings.Left.Width
                    + Paddings.Right.Width;
                _columnWidths[column] = 
                    borders.HorizontalLineStyleLcd * (int)Math.Ceiling((float) maxColumnWidth / (float) borders.HorizontalLineStyleLcd);
            }
        }


#if NET40
        public IList<IColumn> Columns { get; }
#else
        public IReadOnlyList<IColumn> Columns { get; }
#endif
        public IBorders Borders { get; }
        public IMargins Margins { get; }
        public IPaddings Paddings { get; }
        public int GetColumnWidth(IColumn column) => _columnWidths[column];
    }
}