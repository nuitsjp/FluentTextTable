using System;
using System.IO;

namespace FluentTextTable
{
    internal class MarkdownHeaderHorizontalBorder : HorizontalBorder
    {
        internal override void Write(TextWriter writer, ITextTableLayout textTableLayout)
        {
            writer.Write("|");
            foreach (var column in textTableLayout.Columns)
            {
                switch (column.HorizontalAlignment)
                {
                    case HorizontalAlignment.Default:
                        writer.Write(new string('-', textTableLayout.GetColumnWidth(column)));
                        break;
                    case HorizontalAlignment.Left:
                        writer.Write(':');
                        writer.Write(new string('-', textTableLayout.GetColumnWidth(column) - 1));
                        break;
                    case HorizontalAlignment.Center:
                        writer.Write(':');
                        writer.Write(new string('-', textTableLayout.GetColumnWidth(column) - 2));
                        writer.Write(':');
                        break;
                    case HorizontalAlignment.Right:
                        writer.Write(new string('-', textTableLayout.GetColumnWidth(column) - 1));
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