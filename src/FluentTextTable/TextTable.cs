using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace FluentTextTable
{
    public class TextTable<TItem> : ITextTable<TItem>
    {
        internal TextTable(IEnumerable<TextTableColumn<TItem>> columns)
        {
            Columns = columns.ToList();
        }

        private IEnumerable<TextTableColumn<TItem>> Columns { get; }
        public IEnumerable<TItem> DataSource { get; set; }
        public string ToPlanText()
        {
            var writer = new StringWriter();
            WritePlanText(writer);
            return writer.ToString();
        }

        public void WritePlanText(TextWriter writer)
        {
            var rows = new List<TextTableRow<TItem>>();
            foreach (var item in DataSource)
            {
                var row = new TextTableRow<TItem>();
                rows.Add(row);
                foreach (var column in Columns)
                {
                    row.AddCell(column.ToCell(item));
                }
            }

            foreach (var column in Columns)
            {
                column.UpdateWidth(rows);
            }


            var rowSeparatorBuilder = new StringBuilder();
            foreach (var column in Columns)
            {
                rowSeparatorBuilder.Append('+');
                rowSeparatorBuilder.Append(new string('-', column.Width));
            }
            rowSeparatorBuilder.Append("+");

            var rowSeparator = rowSeparatorBuilder.ToString();

            // Write top line.
            writer.WriteLine(rowSeparator);

            // Write header.
            writer.Write("|");
            foreach (var column in Columns)
            {
                writer.Write(" ");
                writer.Write(column.Header);
                writer.Write(new string(' ', column.Width - column.HeaderWidth));

                writer.Write(" |");
            }
            writer.WriteLine();
            writer.WriteLine(rowSeparator);

            foreach (var row in rows)
            {
                writer.Write("|");
                foreach (var column in Columns)
                {
                    writer.Write(" ");

                    var cell = row.Cells[column];

                    int leftPadding;
                    int rightPadding;
                    switch (column.HorizontalAlignment)
                    {
                        case HorizontalAlignment.Left:
                            leftPadding = 0;
                            rightPadding = column.Width - cell.Width;
                            break;
                        case HorizontalAlignment.Center:
                            leftPadding = 0;
                            rightPadding = column.Width - cell.Width;
                            break;
                        case HorizontalAlignment.Right:
                            leftPadding = column.Width - cell.Width;
                            rightPadding = 0;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    writer.Write(new string(' ', leftPadding));
                    writer.Write(cell.Values.First());
                    writer.Write(new string(' ', rightPadding));

                    writer.Write(" |");
                }
                writer.WriteLine();
                writer.WriteLine(rowSeparator);
            }
        }

        public string ToMarkdown()
        {
            throw new NotImplementedException();
        }
    }
}
