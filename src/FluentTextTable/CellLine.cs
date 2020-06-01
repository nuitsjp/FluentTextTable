using System;
using System.IO;
using EastAsianWidthDotNet;

namespace FluentTextTable
{
    internal class CellLine
    {
        internal static readonly CellLine Empty = new CellLine(string.Empty, null);

        private const int Margin = 2;

        private readonly string _value;

        internal string Value => _value;
        internal int Width { get; }

        public CellLine(object value, string format)
        {
            _value = format is null
                ? value?.ToString() ?? string.Empty
                : string.Format(format, value);
            Width = _value.GetWidth() + Margin;
        }

        internal void WritePlanText<TItem>(
            TextTableWriter<TItem> writer,
            Column<TItem> column)
        {
            writer.Write(" ");

            int leftPadding;
            int rightPadding;
            switch (column.HorizontalAlignment)
            {
                case HorizontalAlignment.Default:
                case HorizontalAlignment.Left:
                    leftPadding = 0;
                    rightPadding = writer.GetColumnWidth(column) - Width;
                    break;
                case HorizontalAlignment.Center:
                    leftPadding = (writer.GetColumnWidth(column) - Width) / 2;
                    rightPadding = writer.GetColumnWidth(column) - Width - leftPadding;
                    break;
                case HorizontalAlignment.Right:
                    leftPadding = writer.GetColumnWidth(column) - Width;
                    rightPadding = 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            writer.Write(new string(' ', leftPadding));
            writer.Write(_value);
            writer.Write(new string(' ', rightPadding));
            writer.Write(" ");
        }
        
        internal void WriteMarkdown<TItem>(
            TextTableWriter<TItem> writer,
            Column<TItem> column)
        {
            int leftPadding;
            int rightPadding;
            switch (column.HorizontalAlignment)
            {
                case HorizontalAlignment.Default:
                case HorizontalAlignment.Left:
                    leftPadding = 0;
                    rightPadding = writer.GetColumnWidth(column) - Width;
                    break;
                case HorizontalAlignment.Center:
                    leftPadding = (writer.GetColumnWidth(column) - Width) / 2;
                    rightPadding = writer.GetColumnWidth(column) - Width - leftPadding;
                    break;
                case HorizontalAlignment.Right:
                    leftPadding = writer.GetColumnWidth(column) - Width;
                    rightPadding = 0;
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