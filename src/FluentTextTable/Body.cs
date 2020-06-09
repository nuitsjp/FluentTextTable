using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentTextTable
{
    internal class Body<TItem>
    {
        private readonly IList<Column<TItem>> _columns;

        private readonly Borders _borders;

        private readonly List<Row<TItem>> _rows = new List<Row<TItem>>();

        internal IReadOnlyList<Row<TItem>> Rows => _rows;
        
        internal Body(IList<Column<TItem>> columns, Borders borders, IEnumerable<TItem> items)
        {
            _columns = columns;
            _borders = borders;

            foreach (var item in items)
            {
                _rows.Add(new Row<TItem>(columns, borders, item));
            }
        }
        
        internal int GetColumnWidth(Column<TItem> column) => _rows.Max(x => x.GetColumnWidth(column));

        internal void WritePlaneText(TextWriter textWriter, TextTable<TItem> table)
        {
            if (_rows.Any())
            {
                _rows[0].WritePlanText(textWriter, table);
                for (var i = 1; i < _rows.Count; i++)
                {
                    _borders.InsideHorizontal.Write(textWriter, table, _columns);
                    _rows[i].WritePlanText(textWriter, table);
                }
            }
        }

        internal void WriteMarkdown(TextWriter textWriter, TextTable<TItem> table)
        {
            foreach (var row in _rows)
            {
                row.WriteMarkdown(textWriter, table, _columns);
            }
        }
    }
}