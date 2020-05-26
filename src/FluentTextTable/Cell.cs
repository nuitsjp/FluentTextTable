using System;
using System.IO;
using System.Linq;
using EastAsianWidthDotNet;

namespace FluentTextTable
{
    internal class Cell<TItem>
    {
        private const int Margin = 2;
        public Cell(object value, string format)
        {
            var values =
                value is string stringValue
                    ? stringValue.Split(Environment.NewLine.ToCharArray())
                    : new [] {value};

            Values =
                format is null
                    ? values.Select(x => x?.ToString() ?? string.Empty).ToArray()
                    : values.Select(x => string.Format(format, x)).ToArray();
            Width = Values.Max(x =>x.GetWidth()) + Margin;
        }

        private string[] Values { get; }

        internal int Width { get; }
        internal int Height => Values.Length;

        internal void Write(
            TextWriter writer,
            Column<TItem> column,
            int lineNumber)
        {
            writer.Write(" ");

            int leftPadding;
            int rightPadding;
            switch (column.HorizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    leftPadding = 0;
                    rightPadding = column.Width - Width;
                    break;
                case HorizontalAlignment.Center:
                    leftPadding = 0;
                    rightPadding = column.Width - Width;
                    break;
                case HorizontalAlignment.Right:
                    leftPadding = column.Width - Width;
                    rightPadding = 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            string value;
            switch (column.VerticalAlignment)
            {
                case VerticalAlignment.Top:
                    value = lineNumber < Height
                        ? Values[lineNumber]
                        : string.Empty;
                    break;
                case VerticalAlignment.Center:
                    break;
                case VerticalAlignment.Bottom:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            writer.Write(new string(' ', leftPadding));
            writer.Write(Values[lineNumber]);
            writer.Write(new string(' ', rightPadding));

            writer.Write(" |");

        }
    }
}