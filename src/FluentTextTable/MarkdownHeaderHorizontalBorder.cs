using System;
using System.IO;

namespace FluentTextTable
{
    internal class MarkdownHeaderHorizontalBorder : HorizontalBorder
    {
        internal override void Write<TItem1>(TextWriter writer, ITableInstance<TItem1> tableInstance)
        {
            writer.Write("|");
            foreach (var column in tableInstance.Columns)
            {
                switch (column.HorizontalAlignment)
                {
                    case HorizontalAlignment.Default:
                        writer.Write(new string('-', tableInstance.GetColumnWidth(column)));
                        break;
                    case HorizontalAlignment.Left:
                        writer.Write(':');
                        writer.Write(new string('-', tableInstance.GetColumnWidth(column) - 1));
                        break;
                    case HorizontalAlignment.Center:
                        writer.Write(':');
                        writer.Write(new string('-', tableInstance.GetColumnWidth(column) - 2));
                        writer.Write(':');
                        break;
                    case HorizontalAlignment.Right:
                        writer.Write(new string('-', tableInstance.GetColumnWidth(column) - 1));
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