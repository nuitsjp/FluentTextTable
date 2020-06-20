using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentTextTable
{
    public class RowSet : IRowSet
    {
        private readonly IReadOnlyList<IRow> _rows;

        public RowSet(IReadOnlyList<IRow> rows) => _rows = rows;

        public int GetWidthOf(IColumn column) => _rows.Max(x => x.GetWidthOf(column));

        public void WriteRows(TextWriter writer, ITextTableLayout textTableLayout)
        {
            if (!_rows.Any()) return;
            
            _rows[0].Write(writer, textTableLayout);
            for (var i = 1; i < _rows.Count; i++)
            {
                textTableLayout.Borders.InsideHorizontal.Write(writer, textTableLayout);
                _rows[i].Write(writer, textTableLayout);
            }
        }
    }
}