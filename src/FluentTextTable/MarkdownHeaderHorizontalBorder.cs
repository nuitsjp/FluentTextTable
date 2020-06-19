using System;
using System.IO;

namespace FluentTextTable
{
    internal class MarkdownHeaderHorizontalBorder : HorizontalBorder
    {
        internal override void Write(TextWriter writer, ITableLayout tableLayout)
        {
            writer.Write("|");
            foreach (var column in tableLayout.Columns)
            {
                switch (column.HorizontalAlignment)
                {
                    case HorizontalAlignment.Default:
                        writer.Write(new string('-', tableLayout.GetColumnWidth(column)));
                        break;
                    case HorizontalAlignment.Left:
                        writer.Write(':');
                        writer.Write(new string('-', tableLayout.GetColumnWidth(column) - 1));
                        break;
                    case HorizontalAlignment.Center:
                        writer.Write(':');
                        writer.Write(new string('-', tableLayout.GetColumnWidth(column) - 2));
                        writer.Write(':');
                        break;
                    case HorizontalAlignment.Right:
                        writer.Write(new string('-', tableLayout.GetColumnWidth(column) - 1));
                        writer.Write(':');
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                writer.Write("|");
            }
            writer.WriteLine();
        }
    }
}