using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentTextTable
{
    public class Header : IHeader

    {
        public Header(IEnumerable<IColumn> columns)
        {
            Columns = columns.ToList();
        }

        public IReadOnlyList<IColumn> Columns { get; }
        
        public void Write(TextWriter writer, ITableLayout tableLayout)
        {
            tableLayout.Borders.Left.Write(writer);
        
            Columns[0].WriteHeader(writer, tableLayout);
        
            for (var i = 1; i < Columns.Count; i++)
            {
                tableLayout.Borders.InsideVertical.Write(writer);
                Columns[i].WriteHeader(writer, tableLayout);
            }
            
            tableLayout.Borders.Right.Write(writer);
        
            writer.WriteLine();
        }
    }
}