using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentTextTable
{
    public class TextTableLayout : ITextTableLayout
    {
        private readonly Dictionary<IColumn, int> _columnWidths = new Dictionary<IColumn, int>();
        
        internal TextTableLayout(IReadOnlyList<IColumn> columns, Borders borders, int padding, IRowSet rowSet)
        {
            Borders = borders;
            Columns = columns;
            Padding = padding;
            foreach (var column in Columns)
            {
                _columnWidths[column] = 
                    Math.Max(
                        column.HeaderWidth, 
                        rowSet.GetColumnWidth(column))
                    + Padding * 2;
            }
        }


        public IReadOnlyList<IColumn> Columns { get; }
        public Borders Borders { get; }
        public int Padding { get; }
        public int GetColumnWidth(IColumn column) => _columnWidths[column];
    }
}