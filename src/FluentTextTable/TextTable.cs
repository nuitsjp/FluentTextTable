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
        internal TextTable(IEnumerable<Column<TItem>> columns)
        {
            Columns = columns.ToList();
        }

        private List<Column<TItem>> Columns { get; }
        public IEnumerable<TItem> DataSource { get; set; }
        public string ToPlanText()
        {
            var writer = new StringWriter();
            WritePlanText(writer);
            return writer.ToString();
        }

        public void WritePlanText(TextWriter writer)
        {
            var rows = DataSource.Select(x => new Row<TItem>(x, Columns)).ToList();

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

            // Write Header and table separator.
            writer.WriteLine();
            writer.WriteLine(rowSeparator);

            // Write table.
            foreach (var row in rows)
            {
                // Write row.
                var rowHeight = row.Height;

                // Write line in row.
                for (var lineNumber = 0; lineNumber < rowHeight; lineNumber++)
                {
                    writer.Write("|");
                    foreach (var column in Columns)
                    {
                        row.Cells[column].Write(writer, column, lineNumber);
                    }
                    writer.WriteLine();
                }

                writer.WriteLine(rowSeparator);
            }
        }

        public string ToMarkdown()
        {
            throw new NotImplementedException();
        }
    }
}
