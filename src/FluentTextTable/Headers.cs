using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentTextTable
{
    internal class Headers<TItem>
    {
        private readonly List<Column<TItem>> _columns;
        private readonly Borders _borders;

        internal Headers(List<Column<TItem>> columns, Borders borders)
        {
            _columns = columns;
            _borders = borders;
        }

        internal void Write(TextWriter writer, ITextTable<TItem> table)
        {
            _borders.Left.Write(writer);
            
            _columns[0].WriteHeader(writer, table);
            writer.Write(" ");

            for (var i = 1; i < _columns.Count; i++)
            {
                _borders.InsideVertical.Write(writer);

                _columns[i].WriteHeader(writer, table);
                writer.Write(" ");
            }
            
            _borders.Right.Write(writer);

            writer.WriteLine();
        }
        
        
    }
}