using System;
using System.IO;
using EastAsianWidthDotNet;

namespace FluentTextTable
{
    internal class CellLine<TItem>
    {
        private const int Margin = 2;

        private readonly Column<TItem> _column;

        internal string Value { get; }

        internal int Width { get; }

        internal CellLine(Column<TItem> column, object value)
        {
            _column = column;
            Value = column?.Format is null
                ? value?.ToString() ?? string.Empty
                : string.Format(column.Format, value);
            Width = Value.GetWidth() + Margin;
        }

        internal CellLine(Column<TItem> column)
        {
            _column = column;
            Value = string.Empty;
            Width = Margin;
        }

        internal void WritePlanText(
            TextWriter textWriter,
            ITextTable<TItem> table)
        {
            textWriter.Write(" ");

            int leftPadding;
            int rightPadding;
            switch (_column.HorizontalAlignment)
            {
                case HorizontalAlignment.Default:
                case HorizontalAlignment.Left:
                    leftPadding = 0;
                    rightPadding = table.GetColumnWidth(_column) - Width;
                    break;
                case HorizontalAlignment.Center:
                    leftPadding = (table.GetColumnWidth(_column) - Width) / 2;
                    rightPadding = table.GetColumnWidth(_column) - Width - leftPadding;
                    break;
                case HorizontalAlignment.Right:
                    leftPadding = table.GetColumnWidth(_column) - Width;
                    rightPadding = 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            textWriter.Write(new string(' ', leftPadding));
            textWriter.Write(Value);
            textWriter.Write(new string(' ', rightPadding));
            textWriter.Write(" ");
        }
        
        internal void WriteMarkdown(
            TextWriter textWriter,
            ITextTable<TItem> table)
        {
            int leftPadding;
            int rightPadding;
            switch (_column.HorizontalAlignment)
            {
                case HorizontalAlignment.Default:
                case HorizontalAlignment.Left:
                    leftPadding = 0;
                    rightPadding = table.GetColumnWidth(_column) - Width;
                    break;
                case HorizontalAlignment.Center:
                    leftPadding = (table.GetColumnWidth(_column) - Width) / 2;
                    rightPadding = table.GetColumnWidth(_column) - Width - leftPadding;
                    break;
                case HorizontalAlignment.Right:
                    leftPadding = table.GetColumnWidth(_column) - Width;
                    rightPadding = 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            textWriter.Write(new string(' ', leftPadding));
            textWriter.Write(Value);
            textWriter.Write(new string(' ', rightPadding));
        }
    }
}