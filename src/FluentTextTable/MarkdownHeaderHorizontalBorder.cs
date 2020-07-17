using System;
using System.IO;

namespace FluentTextTable
{
    internal class MarkdownHeaderHorizontalBorder : HorizontalBorder
    {
        internal MarkdownHeaderHorizontalBorder() : base("-")
        {
        }

        public override void Write(TextWriter textWriter, ITextTableLayout textTableLayout)
        {
            textWriter.Write("|");
            foreach (var column in textTableLayout.Columns)
            {
                switch (column.HorizontalAlignment)
                {
                    case HorizontalAlignment.Default:
                        textWriter.Write(new string('-', textTableLayout.GetColumnWidth(column)));
                        break;
                    case HorizontalAlignment.Left:
                        textWriter.Write(':');
                        textWriter.Write(new string('-', textTableLayout.GetColumnWidth(column) - 1));
                        break;
                    case HorizontalAlignment.Center:
                        textWriter.Write(':');
                        textWriter.Write(new string('-', textTableLayout.GetColumnWidth(column) - 2));
                        textWriter.Write(':');
                        break;
                    case HorizontalAlignment.Right:
                        textWriter.Write(new string('-', textTableLayout.GetColumnWidth(column) - 1));
                        textWriter.Write(':');
                        break;
                }
                textWriter.Write("|");
            }
            textWriter.WriteLine();
        }
    }
}