using System;
using System.IO;

namespace FluentTextTable
{
    public class CellLine : ICellLine
    {
        internal static readonly CellLine BlankCellLine = new CellLine(string.Empty); 

        private readonly string _value;
        
        internal CellLine(string value)
        {
            _value = value;
            Width = value.GetWidth();
        }

        public int Width { get; }

        public void Write(
            TextWriter textWriter,
            IColumn column,
            int columnWidth,
            int padding)
        {
            int leftPadding;
            int rightPadding;
            switch (column.HorizontalAlignment)
            {
                case HorizontalAlignment.Default:
                case HorizontalAlignment.Left:
                    leftPadding = padding;
                    rightPadding = columnWidth - Width - leftPadding;
                    break;
                case HorizontalAlignment.Center:
                    leftPadding = (columnWidth - Width) / 2;
                    rightPadding = columnWidth - Width - leftPadding;
                    break;
                case HorizontalAlignment.Right:
                    leftPadding = columnWidth - Width - padding;
                    rightPadding = padding;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            textWriter.Write(new string(' ', leftPadding));
            textWriter.Write(_value);
            textWriter.Write(new string(' ', rightPadding));
        }
    }
}