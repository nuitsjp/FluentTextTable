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
        
        public void Write(TextWriter writer, ITextTableLayout textTableLayout)
        {
            textTableLayout.Borders.Left.Write(writer);
        
            Columns[0].Write(writer, textTableLayout);
        
            for (var i = 1; i < Columns.Count; i++)
            {
                textTableLayout.Borders.InsideVertical.Write(writer);
                Columns[i].Write(writer, textTableLayout);
            }
            
            textTableLayout.Borders.Right.Write(writer);
        
            writer.WriteLine();
        }
    }
}