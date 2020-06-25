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
        
        public void Write(TextWriter textWriter, ITextTableLayout textTableLayout)
        {
            textTableLayout.Margins.Left.Write(textWriter);
            textTableLayout.Borders.Left.Write(textWriter);
        
            Columns[0].Write(textWriter, textTableLayout);
        
            for (var i = 1; i < Columns.Count; i++)
            {
                textTableLayout.Borders.InsideVertical.Write(textWriter);
                Columns[i].Write(textWriter, textTableLayout);
            }
            
            textTableLayout.Borders.Right.Write(textWriter);
            textTableLayout.Margins.Right.Write(textWriter);

            textWriter.WriteLine();
        }
    }
}