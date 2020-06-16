using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentTextTable
{
    public class TextTableInstance<TItem> : TableInstance<TItem>, ITextTableInstance<TItem>
    {
        private readonly TextTable<TItem> _table;
        internal TextTableInstance(TextTable<TItem> table, List<IRow<TItem>> rows) 
            : base(table, rows)
        {
            _table = table;
        }

        public Borders Borders => _table.Borders;
        public void Write(TextWriter writer)
        {
            WriteHorizontalBorder(writer, Borders.Top);
            WriteHeader(writer);
            WriteHorizontalBorder(writer, Borders.HeaderHorizontal);
            WriteRows(writer);
            WriteHorizontalBorder(writer, Borders.Bottom);
        }

        private void WriteHorizontalBorder(TextWriter textWriter, HorizontalBorder border)
        {
            if(!border.IsEnable) return;
            
            if(border.LeftVerticalBorder.IsEnable) textWriter.Write(border.LeftStyle);
            
            var items = new List<string>();
            foreach (var column in Columns)
            {
                items.Add(new string(border.LineStyle, GetColumnWidth(column)));
            }

            textWriter.Write(border.InsideVerticalBorder.IsEnable
                ? string.Join(border.IntersectionStyle.ToString(), items)
                : string.Join(string.Empty, items));

            if(border.RightVerticalBorder.IsEnable) textWriter.Write(border.RightStyle);
            
            textWriter.WriteLine();
        }

        private void WriteHeader(TextWriter writer)
        {
            WriteVerticalBorder(writer, Borders.Left);

            WriteHeader(writer, Columns[0]);

            for (var i = 1; i < Columns.Count; i++)
            {
                WriteVerticalBorder(writer, Borders.InsideVertical);

                WriteHeader(writer, Columns[i]);
            }
            
            WriteVerticalBorder(writer, Borders.Right);

            writer.WriteLine();
        }
         
        private static void WriteVerticalBorder(TextWriter writer, VerticalBorder border)
        {
            if(border.IsEnable) writer.Write(border.LineStyle);
        }

        private void WriteHeader(TextWriter writer, IColumn<TItem> column)
        {
            writer.Write(new string(' ', Padding));
            writer.Write(column.Name);
            writer.Write(new string(' ', GetColumnWidth(column) - column.HeaderWidth - Padding));
        }

        private void WriteRows(TextWriter textWriter)
        {
            if (Rows.Any())
            {
                WriteRow(textWriter, Rows[0]);
                for (var i = 1; i < Rows.Count; i++)
                {
                    WriteHorizontalBorder(textWriter, Borders.InsideHorizontal);
                    WriteRow(textWriter, Rows[i]);
                }
            }
        }


        private void WriteRow(TextWriter textWriter, IRow<TItem> row)
        {
            for (var lineNumber = 0; lineNumber < row.Height; lineNumber++)
            {
                WriteVerticalBorder(textWriter, Borders.Left);

                WriteCell(textWriter, row, Columns[0], lineNumber, GetColumnWidth(Columns[0]));

                for (int i = 1; i < Columns.Count; i++)
                {
                    var column = Columns[i];
                    WriteVerticalBorder(textWriter, Borders.InsideVertical);
                    WriteCell(textWriter, row, column, lineNumber, GetColumnWidth(column));
                }

                WriteVerticalBorder(textWriter, Borders.Right);
                
                textWriter.WriteLine();
            }
        }

        private void WriteCell(
            TextWriter textWriter,
            IRow<TItem> row,
            IColumn<TItem> column,
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

            WriteCellLine(textWriter, cellLine, column, columnWidth);

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

        private void WriteCellLine(
            TextWriter textWriter,
            CellLine cellLine,
            IColumn<TItem> column,
            int columnWidth)
        {
            int leftPadding;
            int rightPadding;
            switch (column.HorizontalAlignment)
            {
                case HorizontalAlignment.Default:
                case HorizontalAlignment.Left:
                    leftPadding = Padding;
                    rightPadding = columnWidth - cellLine.Width - leftPadding;
                    break;
                case HorizontalAlignment.Center:
                    leftPadding = (columnWidth - cellLine.Width) / 2;
                    rightPadding = columnWidth - cellLine.Width - leftPadding;
                    break;
                case HorizontalAlignment.Right:
                    leftPadding = columnWidth - cellLine.Width - Padding;
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