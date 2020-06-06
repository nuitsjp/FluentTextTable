using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentTextTable
{
    internal class Row<TItem>
    {
        private readonly IList<Column<TItem>> _columns;

        private readonly Borders _borders;

        private readonly Dictionary<Column<TItem>, Cell<TItem>> _cells = new Dictionary<Column<TItem>, Cell<TItem>>();
        private readonly int _height;

        internal Row(IList<Column<TItem>> columns, Borders borders, TItem item)
        {
            _columns = columns;
            _borders = borders;

            foreach (var column in _columns)
            {
                _cells[column] = new Cell<TItem>(column, item);
            }

            _height = _cells.Values.Max(x => x.Height);
        }

        internal int GetColumnWidth(Column<TItem> column) => _cells[column].Width;

        internal void WritePlanText(TextWriter textWriter, ITextTable<TItem> table)
        {
            for (var lineNumber = 0; lineNumber < _height; lineNumber++)
            {
                _borders.Left.Write(textWriter);

                _cells[_columns.First()].WritePlanText(textWriter, table, _height, lineNumber);
                
                foreach (var column in _columns.Skip(1))
                {
                    _borders.InsideVertical.Write(textWriter);
                    _cells[column].WritePlanText(textWriter, table,  _height, lineNumber);
                }

                _borders.Right.Write(textWriter);
                
                textWriter.WriteLine();
            }
        }
        
        internal void WriteMarkdown(TextWriter textWriter, ITextTable<TItem> table, IList<Column<TItem>> columns)
        {
            textWriter.Write("|");
            foreach (var column in columns)
            {
                _cells[column].WriteMarkdown(textWriter, table, column);
            }
            textWriter.WriteLine();
        }
    }
}