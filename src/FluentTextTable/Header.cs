using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentTextTable
{
    internal class Header
    {
        private readonly List<Column> _columns;

        internal Header(List<Column> columns)
        {
            _columns = columns;
        }

        internal void Write(TextWriter writer, Borders borders)
        {
            borders.Left.Write(writer);
            
            _columns.First().WriteHeader(writer);
            writer.Write(" ");
            
            foreach (var column in _columns.Skip(1))
            {
                borders.InsideVertical.Write(writer);

                column.WriteHeader(writer);
                writer.Write(" ");
            }
            
            borders.Right.Write(writer);

            writer.WriteLine();


        }
    }
}