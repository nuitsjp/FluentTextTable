using System;
using System.IO;
using EastAsianWidthDotNet;

namespace FluentTextTable
{
    public class CellLine
    {
        internal static readonly CellLine BlankCellLine = new CellLine(string.Empty); 

        private readonly string _values;
        
        internal CellLine(string values)
        {
            _values = values;
            Width = values.GetWidth();
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
            writer.Write(_values);
            writer.Write(new string(' ', rightPadding));
        }
    }
}