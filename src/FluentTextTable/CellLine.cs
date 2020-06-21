using System;
using System.IO;

namespace FluentTextTable
{
    public class CellLine
    {
        internal static readonly CellLine BlankCellLine = new CellLine(string.Empty); 

        private readonly string _value;
        
        internal CellLine(string value)
        {
            _value = value;
            Width = value.GetWidth();
        }

        public int Width { get; }

        internal void Write(
            TextWriter writer,
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

            writer.Write(new string(' ', leftPadding));
            writer.Write(_value);
            writer.Write(new string(' ', rightPadding));
        }
    }
}