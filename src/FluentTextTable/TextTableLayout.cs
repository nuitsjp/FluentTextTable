using System;
using System.Collections.Generic;

namespace FluentTextTable
{
    public class TextTableLayout : ITextTableLayout
    {
        private readonly Dictionary<IColumn, int> _columnWidths = new Dictionary<IColumn, int>();
        
        internal TextTableLayout(IReadOnlyList<IColumn> columns, IBorders borders, int padding, IRowSet rowSet)
        {
            Borders = borders;
            Columns = columns;
            Padding = padding;
            foreach (var column in Columns)
            {
                var maxColumnWidth =
                    Math.Max(
                        column.Width,
                        rowSet.GetMaxCellWidth(column))
                    + Padding * 2;
                _columnWidths[column] = 
                    borders.HorizontalLineStyleLcd * (int)Math.Ceiling((float) maxColumnWidth / (float) borders.HorizontalLineStyleLcd);
            }
        }


        public IReadOnlyList<IColumn> Columns { get; }
        public IBorders Borders { get; }
        public int Padding { get; }
        public int GetColumnWidth(IColumn column) => _columnWidths[column];
    }
}