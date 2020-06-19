using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentTextTable
{
    public class ColumnWidths : IColumnWidths
    {
        private readonly Dictionary<IColumn, int> _columnWidths = new Dictionary<IColumn, int>();
        
        internal ColumnWidths(ITable table, IEnumerable<IRow> rows)
        {
            Columns = table.Columns;
            foreach (var column in table.Columns)
            {
                _columnWidths[column] = 
                    Math.Max(
                        column.HeaderWidth, 
                        rows.Max(x => x.GetColumnWidth(column)))
                    + table.Padding * 2;
            }
        }


        public IEnumerable<IColumn> Columns { get; }
        public int this[IColumn column] => _columnWidths[column];
    }
}