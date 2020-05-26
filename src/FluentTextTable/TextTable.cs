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
                row.Write(writer, _columns.Select(x => (Column)x).ToList());

                writer.WriteLine(rowSeparator);
            }
        }

        public string ToMarkdown()
        {
            throw new NotImplementedException();
        }
    }
}
