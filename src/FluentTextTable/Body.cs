using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentTextTable
{
    internal class Body<TItem>
    {
        private readonly IList<Column<TItem>> _columns;

        private readonly IList<Row<TItem>> _rows = new List<Row<TItem>>();
        
        private readonly Dictionary<Column<TItem>, int> _widths = new Dictionary<Column<TItem>, int>();

        internal Body(IList<Column<TItem>> columns, IEnumerable<TItem> items)
        {
            _columns = columns;
            
            foreach (var item in items)
            {
                _rows.Add(Row<TItem>.Create(item, columns));
            }

            foreach (var column in columns)
            {
                _widths[column] = _rows.Max(x => x.GetWidth(column));
            }
        }

        internal int GetWidth(Column<TItem> column) => _widths[column];

        internal void WritePlaneText(TextTableWriter<TItem> writer, Borders borders)
        {
            if (_rows.Any())
            {
                _rows[0].WritePlanText(writer, _columns, borders);
                for (var i = 1; i < _rows.Count; i++)
                {
                    borders.InsideHorizontal.Write(writer, _columns);
                    _rows[i].WritePlanText(writer, _columns, borders);
                }
            }
        }

        internal void WriteMarkdown(TextTableWriter<TItem> writer)
        {
            foreach (var row in _rows)
            {
                row.WriteMarkdown(writer, _columns);
            }
        }
    }
}