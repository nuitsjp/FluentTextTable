using System.Collections.Generic;
using System.IO;

namespace FluentTextTable
{
    internal class Borders
    {
        internal Borders(HorizontalBorder top, HorizontalBorder headerHorizontal, HorizontalBorder insideHorizontal, HorizontalBorder bottom, VerticalBorder left, VerticalBorder insideVertical, VerticalBorder right)
        {
            Top = top;
            HeaderHorizontal = headerHorizontal;
            InsideHorizontal = insideHorizontal;
            Bottom = bottom;
            Left = left;
            InsideVertical = insideVertical;
            Right = right;
        }

        internal HorizontalBorder Top { get; }
        internal HorizontalBorder HeaderHorizontal { get; }
        internal HorizontalBorder InsideHorizontal { get; }
        internal HorizontalBorder Bottom { get; }
        internal VerticalBorder Left { get; }
        internal VerticalBorder InsideVertical { get; }
        internal VerticalBorder Right { get; }

        internal void WriteTop(TextWriter writer, IEnumerable<Column> columns)
        {
            Top.Write(writer, columns, this);
        }

        internal void WriteHeaderHorizontal(TextWriter writer, IEnumerable<Column> columns)
        {
            HeaderHorizontal.Write(writer, columns, this);
        }
        
        internal void WriteInsideHorizontal(TextWriter writer, IEnumerable<Column> columns)
        {
            InsideHorizontal.Write(writer, columns, this);
        }
        
        internal void WriteBottom(TextWriter writer, IEnumerable<Column> columns)
        {
            Bottom.Write(writer, columns, this);
        }
    }
}