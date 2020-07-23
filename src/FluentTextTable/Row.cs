using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentTextTable
{
    public class Row : IRow
    {
        private readonly int _height;

#if NET40
        private readonly IDictionary<IColumn, ICell> _cells;
#else
        private readonly IReadOnlyDictionary<IColumn, ICell> _cells;
#endif

#if NET40
        internal Row(IDictionary<IColumn, ICell> cells)
#else
        internal Row(IReadOnlyDictionary<IColumn, ICell> cells)
#endif
        {
            _cells = cells;
            _height = _cells.Values.Max(x => x.Height);
        }


        public int GetCellWidth(IColumn column) => _cells[column].Width;
        
        public void Write(TextWriter textWriter, ITextTableLayout textTableLayout)
        {
            for (var lineNumber = 0; lineNumber < _height; lineNumber++)
            {
                textTableLayout.Margins.Left.Write(textWriter);
                textTableLayout.Borders.Left.Write(textWriter);

                _cells[textTableLayout.Columns[0]].Write(textWriter, _height, lineNumber, textTableLayout);

                for (var i = 1; i < textTableLayout.Columns.Count; i++)
                {
                    var column = textTableLayout.Columns[i];
                    textTableLayout.Borders.InsideVertical.Write(textWriter);
                    _cells[column].Write(textWriter, _height, lineNumber, textTableLayout);
                }

                textTableLayout.Borders.Right.Write(textWriter);
                textTableLayout.Margins.Right.Write(textWriter);
                
                textWriter.WriteLine();
            }
        }

    }
}