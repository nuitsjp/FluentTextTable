using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentTextTable.PlanText
{
    internal static class TextWriterExtensions
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

        internal static void WriteHeader<TItem>(this ITextTable<TItem> table, TextWriter writer, IRowSet<TItem> rowSet)
        {
            table.Borders.Left.Write(writer);

            WriteHeader(writer, rowSet, table.Columns[0]);
            writer.Write(" ");

            for (var i = 1; i < table.Columns.Count; i++)
            {
                table.Borders.InsideVertical.Write(writer);

                WriteHeader(writer, rowSet, table.Columns[i]);
                writer.Write(" ");
            }
            
            table.Borders.Right.Write(writer);

            writer.WriteLine();
        }
         
        private static void WriteHeader<TItem>(TextWriter writer, IRowSet<TItem> rowSet, IColumn<TItem> column)
        {
            writer.Write(" ");
            writer.Write(column.Name);
            writer.Write(new string(' ', rowSet.GetColumnWidth(column) - column.HeaderWidth));
        }

        internal static void Write<TItem>(this RowSet<TItem> rowSet, TextWriter textWriter, ITextTable<TItem> table)
        {
            if (rowSet.Rows.Any())
            {
                rowSet.Rows[0].Write(textWriter, table, rowSet);
                for (var i = 1; i < rowSet.Rows.Count; i++)
                {
                    table.Borders.InsideHorizontal.Write(textWriter, table, rowSet);
                    rowSet.Rows[i].Write(textWriter, table, rowSet);
                }
            }
        }


        private static void Write<TItem>(this IRow<TItem> row, TextWriter textWriter, ITextTable<TItem> table, IRowSet<TItem> rowSet)
        {
            for (var lineNumber = 0; lineNumber < row.Height; lineNumber++)
            {
                table.Borders.Left.Write(textWriter);

                row.WriteCell(table.Columns[0], textWriter, lineNumber, rowSet.GetColumnWidth(table.Columns[0]));

                for (int i = 1; i < table.Columns.Count; i++)
                {
                    var column = table.Columns[i];
                    table.Borders.InsideVertical.Write(textWriter);
                    row.WriteCell(column, textWriter, lineNumber, rowSet.GetColumnWidth(table.Columns[i]));
                }

                table.Borders.Right.Write(textWriter);
                
                textWriter.WriteLine();
            }
        }

        private static void WriteCell<TItem>(
            this IRow<TItem> row,
            IColumn<TItem> column,
            TextWriter textWriter,
            int lineNumber,
            int columnWidth)
        {
            var cell = row.Cells[column];
            CellLine cellLine;
            switch (column.VerticalAlignment)
            {
                case VerticalAlignment.Top:
                    cellLine = GetTopCellLine();
                    break;
                case VerticalAlignment.Center:
                    cellLine = GetCenterCellLine();
                    break;
                case VerticalAlignment.Bottom:
                    cellLine = GetBottomCellLine();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            cellLine.Write(textWriter, column, columnWidth);

            CellLine GetTopCellLine()
            {
                return lineNumber < cell.Height
                    ? cell.GetCellLine(lineNumber)
                    : CellLine.BlankCellLine;
            }

            CellLine GetCenterCellLine()
            {
                var indent = (row.Height - cell.Height) / 2;
                var localLineNumber = lineNumber - indent;
                if (localLineNumber < 0)
                {
                    return CellLine.BlankCellLine;
                }

                if (cell.Height <= localLineNumber)
                {
                    return CellLine.BlankCellLine;
                }

                return cell.GetCellLine(localLineNumber);
            }

            CellLine GetBottomCellLine()
            {
                var indent = row.Height - cell.Height;
                var localLineNumber = lineNumber - indent;
                if (localLineNumber < 0)
                {
                    return CellLine.BlankCellLine;
                }

                return cell.GetCellLine(localLineNumber);
            }
        }

        private static void Write<TItem>(
            this CellLine cellLine,
            TextWriter textWriter,
            IColumn<TItem> column,
            int columnWidth)
        {
            textWriter.Write(" ");

            int leftPadding;
            int rightPadding;
            switch (column.HorizontalAlignment)
            {
                case HorizontalAlignment.Default:
                case HorizontalAlignment.Left:
                    leftPadding = 0;
                    rightPadding = columnWidth - cellLine.Width;
                    break;
                case HorizontalAlignment.Center:
                    leftPadding = (columnWidth - cellLine.Width) / 2;
                    rightPadding = columnWidth - cellLine.Width - leftPadding;
                    break;
                case HorizontalAlignment.Right:
                    leftPadding = columnWidth - cellLine.Width;
                    rightPadding = 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            textWriter.Write(new string(' ', leftPadding));
            textWriter.Write(cellLine.Value);
            textWriter.Write(new string(' ', rightPadding));
            textWriter.Write(" ");
        }
    }
}