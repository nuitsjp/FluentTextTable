using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentTextTable.PlanText
{
    internal static class TextWriterExtensions
    {
        internal static void Write<TItem>(this HorizontalBorder border, TextWriter textWriter, ITextTableLayout<TItem> layout)
        {
            if(!border.IsEnable) return;
            
            if(border.LeftVerticalBorder.IsEnable) textWriter.Write(border.LeftStyle);
            
            var items = new List<string>();
            foreach (var column in layout.Columns)
            {
                items.Add(new string(border.LineStyle, layout.GetColumnWidth(column)));
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

        internal static void WriteHeader<TItem>(this ITextTableLayout<TItem> layout, TextWriter writer)
        {
            layout.Borders.Left.Write(writer);

            WriteHeader(writer, layout, layout.Columns[0]);
            writer.Write(" ");

            for (var i = 1; i < layout.Columns.Count; i++)
            {
                layout.Borders.InsideVertical.Write(writer);

                WriteHeader(writer, layout, layout.Columns[i]);
                writer.Write(" ");
            }
            
            layout.Borders.Right.Write(writer);

            writer.WriteLine();
        }
         
        internal static void Write<TItem>(this List<Row<TItem>> rows, TextWriter textWriter, ITextTableLayout<TItem> layout)
        {
            if (rows.Any())
            {
                rows[0].Write(textWriter, layout);
                for (var i = 1; i < rows.Count; i++)
                {
                    layout.Borders.InsideHorizontal.Write(textWriter, layout);
                    rows[i].Write(textWriter, layout);
                }
            }
        }


        private static void Write<TItem>(this Row<TItem> row, TextWriter textWriter, ITextTableLayout<TItem> layout)
        {
            for (var lineNumber = 0; lineNumber < row.Height; lineNumber++)
            {
                layout.Borders.Left.Write(textWriter);

                row.WriteCell(layout.Columns[0], textWriter, layout, lineNumber);

                for (int i = 1; i < layout.Columns.Count; i++)
                {
                    var column = layout.Columns[i];
                    layout.Borders.InsideVertical.Write(textWriter);
                    row.WriteCell(column, textWriter, layout, lineNumber);
                }

                layout.Borders.Right.Write(textWriter);
                
                textWriter.WriteLine();
            }
        }

        private static void WriteCell<TItem>(
            this Row<TItem> row,
            Column<TItem> column,
            TextWriter textWriter,
            ITextTableLayout<TItem> layout,
            int lineNumber)
        {
            var cell = row.Cells[column];
            CellLine<TItem> cellLine;
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

            cellLine.Write(textWriter, layout, column);

            CellLine<TItem> GetTopCellLine()
            {
                return lineNumber < cell.Height
                    ? cell.GetCellLine(lineNumber)
                    : CellLine<TItem>.BlankCellLine;
            }

            CellLine<TItem> GetCenterCellLine()
            {
                var indent = (row.Height - cell.Height) / 2;
                var localLineNumber = lineNumber - indent;
                if (localLineNumber < 0)
                {
                    return CellLine<TItem>.BlankCellLine;
                }

                if (cell.Height <= localLineNumber)
                {
                    return CellLine<TItem>.BlankCellLine;
                }

                return cell.GetCellLine(localLineNumber);
            }

            CellLine<TItem> GetBottomCellLine()
            {
                var indent = row.Height - cell.Height;
                var localLineNumber = lineNumber - indent;
                if (localLineNumber < 0)
                {
                    return CellLine<TItem>.BlankCellLine;
                }

                return cell.GetCellLine(localLineNumber);
            }
        }
        
        internal static void WriteHeader<TItem>(TextWriter writer, ITextTableLayout<TItem> layout, Column<TItem> column)
        {
            writer.Write(" ");
            writer.Write(column.Name);
            writer.Write(new string(' ', layout.GetColumnWidth(column) - column.HeaderWidth));
        }
        
        internal static void Write<TItem>(
            this CellLine<TItem> cellLine,
            TextWriter textWriter,
            ITextTableLayout<TItem> layout,
            Column<TItem> column)
        {
            textWriter.Write(" ");

            int leftPadding;
            int rightPadding;
            switch (column.HorizontalAlignment)
            {
                case HorizontalAlignment.Default:
                case HorizontalAlignment.Left:
                    leftPadding = 0;
                    rightPadding = layout.GetColumnWidth(column) - cellLine.Width;
                    break;
                case HorizontalAlignment.Center:
                    leftPadding = (layout.GetColumnWidth(column) - cellLine.Width) / 2;
                    rightPadding = layout.GetColumnWidth(column) - cellLine.Width - leftPadding;
                    break;
                case HorizontalAlignment.Right:
                    leftPadding = layout.GetColumnWidth(column) - cellLine.Width;
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