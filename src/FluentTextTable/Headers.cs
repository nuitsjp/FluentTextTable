using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentTextTable
{
    internal class Headers<TItem>
    {
        private readonly List<Column<TItem>> _columns;

        internal Headers(List<Column<TItem>> columns)
        {
            _columns = columns;
        }

        internal void Write(TextWriter writer, Borders borders, Body<TItem> body)
        {
            borders.Left.Write(writer);
            
            _columns[0].WriteHeader(writer, body);
            writer.Write(" ");

            for (var i = 1; i < _columns.Count; i++)
            {
                borders.InsideVertical.Write(writer);

                _columns[i].WriteHeader(writer, body);
                writer.Write(" ");
            }
            
            borders.Right.Write(writer);

            writer.WriteLine();
        }
        
        
    }
}