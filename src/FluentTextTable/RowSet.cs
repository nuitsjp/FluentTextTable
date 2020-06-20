using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentTextTable
{
    public class RowSet : IRowSet
    {
        private readonly IReadOnlyList<IRow> _rows;

        public RowSet(IReadOnlyList<IRow> rows) => _rows = rows;

        public int GetColumnWidth(IColumn column) => _rows.Max(x => x.GetColumnWidth(column));

        public void WriteRows(TextWriter writer, ITextTableLayout textTableLayout)
        {
            if (_rows.Any())
            {
                _rows[0].Write(writer, textTableLayout);
                for (var i = 1; i < _rows.Count; i++)
                {
                    textTableLayout.Borders.InsideHorizontal.Write(writer, textTableLayout);
                    _rows[i].Write(writer, textTableLayout);
                }
            }
        }
    }
}