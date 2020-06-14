using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EastAsianWidthDotNet;

namespace FluentTextTable.Markdown
{
    internal static class MarkdownWriterExtensions
    {
        internal static void Write<TItem>(this HorizontalBorder border, TextWriter textWriter, ITable<TItem> table, IRowSet<TItem> rowSet)
        {
            if(!border.IsEnable) return;
            
            if(border.LeftVerticalBorder.IsEnable) textWriter.Write(border.LeftStyle);
            
            var items = new List<string>();
            foreach (var column in table.Columns)
            {
                items.Add(new string(border.LineStyle, rowSet.GetColumnWidth(column)));
            }

            textWriter.Write(border.InsideVerticalBorder.IsEnable
                ? string.Join(border.IntersectionStyle.ToString(), items)
                : string.Join(string.Empty, items));

            if(border.RightVerticalBorder.IsEnable) textWriter.Write(border.RightStyle);
            
            textWriter.WriteLine();
        }
        
        private static void Write(this VerticalBorder border, TextWriter writer)
        {
            if(border.IsEnable) writer.Write(border.LineStyle);
        }

        
        internal static void WriteHeader<TItem>(this ITable<TItem> table, TextWriter textWriter, IRowSet<TItem> rowSet)
        {
            var headerSeparator = new StringBuilder();
            textWriter.Write("|");
            headerSeparator.Append("|");
            foreach (var column in table.Columns)
            {
                textWriter.Write(" ");

                textWriter.Write(column.Name);
                textWriter.Write(new string(' ', rowSet.GetColumnWidth(column) - column.Name.GetWidth() - 2)); // TODO Fix -> column.Header.GetWidth() - 2

                switch (column.HorizontalAlignment)
                {
                    case HorizontalAlignment.Default:
                        headerSeparator.Append(new string('-', rowSet.GetColumnWidth(column)));
                        break;
                    case HorizontalAlignment.Left:
                        headerSeparator.Append(':');
                        headerSeparator.Append(new string('-', rowSet.GetColumnWidth(column) - 1));
                        break;
                    case HorizontalAlignment.Center:
                        headerSeparator.Append(':');
                        headerSeparator.Append(new string('-', rowSet.GetColumnWidth(column) - 2));
                        headerSeparator.Append(':');
                        break;
                    case HorizontalAlignment.Right:
                        headerSeparator.Append(new string('-', rowSet.GetColumnWidth(column) - 1));
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
         
        internal static void Write<TItem>(this IRowSet<TItem> rowSet, TextWriter textWriter, ITable<TItem> table)
        {
            foreach (var row in rowSet.Rows)
            {
                row.Write(textWriter, table, rowSet);
            }
        }


        private static void Write<TItem>(this IRow<TItem> row, TextWriter textWriter, ITable<TItem> table, IRowSet<TItem> rowSet)
        {
            textWriter.Write("|");
            foreach (var column in table.Columns)
            {
                row.WriteCell(column, textWriter, rowSet);
            }
            textWriter.WriteLine();
        }

        private static void WriteCell<TItem>(
            this IRow<TItem> row,
            IColumn<TItem> column,
            TextWriter textWriter,
            IRowSet<TItem> rowSet)
        {
            var cell = row.Cells[column];
            textWriter.Write(' ');
            if (cell.Height == 1)
            {
                // In the case of 1line, padding should match the width of the column.
                cell.GetCellLine(0).Write(textWriter, rowSet, column);
            }
            else
            {
                // If you're multi-line in Markdown, you can't match the widths, so it doesn't padding.
                // Simply combine them with <br> to describe them.
                textWriter.Write(string.Join("<br>", cell.GetCellLines().Select(x => x.Value)));
            }
            textWriter.Write(" |");

        }
        
        internal static void WriteHeader<TItem>(TextWriter writer, IRowSet<TItem> rowSet, IColumn<TItem> column)
        {
            writer.Write(" ");
            writer.Write(column.Name);
            writer.Write(new string(' ', rowSet.GetColumnWidth(column) - column.HeaderWidth));
        }

        private static void Write<TItem>(
            this CellLine cellLine,
            TextWriter textWriter,
            IRowSet<TItem> rowSet,
            IColumn<TItem> column)
        {
            int leftPadding;
            int rightPadding;
            switch (column.HorizontalAlignment)
            {
                case HorizontalAlignment.Default:
                case HorizontalAlignment.Left:
                    leftPadding = 0;
                    rightPadding = rowSet.GetColumnWidth(column) - cellLine.Width;
                    break;
                case HorizontalAlignment.Center:
                    leftPadding = (rowSet.GetColumnWidth(column) - cellLine.Width) / 2;
                    rightPadding = rowSet.GetColumnWidth(column) - cellLine.Width - leftPadding;
                    break;
                case HorizontalAlignment.Right:
                    leftPadding = rowSet.GetColumnWidth(column) - cellLine.Width;
                    rightPadding = 0;
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