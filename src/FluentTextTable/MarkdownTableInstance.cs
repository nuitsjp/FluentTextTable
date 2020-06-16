using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EastAsianWidthDotNet;

namespace FluentTextTable
{
    public class MarkdownTableInstance<TItem> : TableInstance<TItem>
    {
        public MarkdownTableInstance(MarkdownTable<TItem> table, List<IRow<TItem>> rows) 
            : base(table, rows)
        {
        }

        public void Write(TextWriter textWriter)
        {
            WriteHeader(textWriter);

            foreach (var row in Rows)
            {
                WriteRow(row, textWriter);
            }
        }

        private void WriteHeader(TextWriter textWriter)
        {
            var headerSeparator = new StringBuilder();
            textWriter.Write("|");
            headerSeparator.Append("|");
            foreach (var column in Columns)
            {
                textWriter.Write(new string(' ', Padding));

                textWriter.Write(column.Name);
                textWriter.Write(new string(' ', GetColumnWidth(column) - column.Name.GetWidth() - Padding - 1));

                switch (column.HorizontalAlignment)
                {
                    case HorizontalAlignment.Default:
                        headerSeparator.Append(new string('-', GetColumnWidth(column)));
                        break;
                    case HorizontalAlignment.Left:
                        headerSeparator.Append(':');
                        headerSeparator.Append(new string('-', GetColumnWidth(column) - 1));
                        break;
                    case HorizontalAlignment.Center:
                        headerSeparator.Append(':');
                        headerSeparator.Append(new string('-', GetColumnWidth(column) - 2));
                        headerSeparator.Append(':');
                        break;
                    case HorizontalAlignment.Right:
                        headerSeparator.Append(new string('-', GetColumnWidth(column) - 1));
                        headerSeparator.Append(':');
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                textWriter.Write(" |");
                headerSeparator.Append("|");
            }
            textWriter.WriteLine();
            textWriter.WriteLine(headerSeparator.ToString());
        }
         
        private void WriteRow(IRow<TItem> row, TextWriter textWriter)
        {
            textWriter.Write("|");
            foreach (var column in Columns)
            {
                WriteCell(textWriter, row, column);
            }
            textWriter.WriteLine();
        }

        private void WriteCell(
            TextWriter textWriter,
            IRow<TItem> row,
            IColumn<TItem> column)
        {
            var cell = row.Cells[column];
            if (cell.Height == 1)
            {
                // In the case of 1line, padding should match the width of the column.
                WriteCellLine(textWriter, cell.GetCellLine(0), column);
            }
            else
            {
                // If you're multi-line in Markdown, you can't match the widths, so it doesn't padding.
                // Simply combine them with <br> to describe them.
                textWriter.Write(new string(' ', Padding));
                textWriter.Write(string.Join("<br>", cell.GetCellLines().Select(x => x.Value)));
                textWriter.Write(new string(' ', Padding));
            }
            textWriter.Write("|");
        }
        
        private void WriteCellLine(
            TextWriter textWriter,
            CellLine cellLine,
            IColumn<TItem> column)
        {
            int leftPadding;
            int rightPadding;
            switch (column.HorizontalAlignment)
            {
                case HorizontalAlignment.Default:
                case HorizontalAlignment.Left:
                    leftPadding = Padding;
                    rightPadding = GetColumnWidth(column) - cellLine.Width - Padding;
                    break;
                case HorizontalAlignment.Center:
                    leftPadding = (GetColumnWidth(column) - cellLine.Width) / 2;
                    rightPadding = GetColumnWidth(column) - cellLine.Width - leftPadding;
                    break;
                case HorizontalAlignment.Right:
                    leftPadding = GetColumnWidth(column) - cellLine.Width - Padding;
                    rightPadding = Padding;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            textWriter.Write(new string(' ', leftPadding));
            textWriter.Write(cellLine.Value);
            textWriter.Write(new string(' ', rightPadding));
        }
 
    }
}