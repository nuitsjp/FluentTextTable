using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentTextTable
{
    public class RowSet : IRowSet
    {
        private readonly IReadOnlyList<IRow> _rows;

        public RowSet(IReadOnlyList<IRow> rows) => _rows = rows;

        public int GetMaxCellWidth(IColumn column) =>
            _rows.Any() 
                ? _rows.Max(x => x.GetCellWidth(column)) 
                : 0;

        public void WriteRows(TextWriter textWriter, ITextTableLayout textTableLayout)
        {
            if (!_rows.Any()) return;
            
            _rows[0].Write(textWriter, textTableLayout);
            for (var i = 1; i < _rows.Count; i++)
            {
                textTableLayout.Borders.InsideHorizontal.Write(textWriter, textTableLayout);
                _rows[i].Write(textWriter, textTableLayout);
            }
        }
    }
}