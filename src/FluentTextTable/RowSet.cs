using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentTextTable
{
    public class RowSet : IRowSet
    {
#if NET40
        private readonly IList<IRow> _rows;
#else
        private readonly IReadOnlyList<IRow> _rows;
#endif

#if NET40
        public RowSet(IList<IRow> rows) => _rows = rows;
#else
        public RowSet(IReadOnlyList<IRow> rows) => _rows = rows;
#endif

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