using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FluentTextTable
{
    public class TextTable<TItem> : ITextTable<TItem>
    {
        private readonly List<Column> _columns;
        private readonly Dictionary<Column, MemberAccessor<TItem>> _memberAccessors;

        internal TextTable(TextTableConfig<TItem> config)
        {
            _columns = config.Columns;
            _memberAccessors = config.MemberAccessors;
        }

        public IEnumerable<TItem> DataSource { get; set; }


        public string ToPlanText()
        {
            var writer = new StringWriter();
            WritePlanText(writer);
            return writer.ToString();
        }

        public void WritePlanText(TextWriter writer)
        {
            var rows = DataSource.Select(x => Row.Create(x, _memberAccessors)).ToList();

            foreach (var column in _columns)
            {
                column.UpdateWidth(rows);
            }


            var rowSeparatorBuilder = new StringBuilder();
            foreach (var column in _columns)
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
            foreach (var column in _columns)
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
                row.WritePlanText(writer, _columns);

                writer.WriteLine(rowSeparator);
            }
        }

        public string ToMarkdown()
        {
            var writer = new StringWriter();
            WriteMarkdown(writer);
            return writer.ToString();
        }

        public void WriteMarkdown(TextWriter writer)
        {
            var rows = DataSource.Select(x => Row.Create(x, _memberAccessors)).ToList();

            foreach (var column in _columns)
            {
                column.UpdateWidth(rows);
            }


            // Write header and separator.
            var headerSeparator = new StringBuilder();
            writer.Write("|");
            headerSeparator.Append("|");
            foreach (var column in _columns)
            {
                writer.Write(" ");

                writer.Write(column.Header);
                writer.Write(new string(' ', column.Width - column.HeaderWidth));

                switch (column.HorizontalAlignment)
                {
                    case HorizontalAlignment.Default:
                        headerSeparator.Append(new string('-', column.Width));
                        break;
                    case HorizontalAlignment.Left:
                        headerSeparator.Append(':');
                        headerSeparator.Append(new string('-', column.Width - 1));
                        break;
                    case HorizontalAlignment.Center:
                        headerSeparator.Append(':');
                        headerSeparator.Append(new string('-', column.Width - 2));
                        headerSeparator.Append(':');
                        break;
                    case HorizontalAlignment.Right:
                        headerSeparator.Append(new string('-', column.Width - 1));
                        headerSeparator.Append(':');
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                writer.Write(" |");
                headerSeparator.Append("|");
            }
            writer.WriteLine();
            writer.WriteLine(headerSeparator.ToString());

            // Write table.
            foreach (var row in rows)
            {
                row.WriteMarkdown(writer, _columns);
            }
        }
    }
}
