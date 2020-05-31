using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FluentTextTable
{
    public class TextTable<TItem> : ITextTable<TItem>
    {
        private readonly List<ColumnConfig> _columns;
        private readonly Header _header;
        private readonly Borders _borders;
        private readonly Dictionary<ColumnConfig, MemberAccessor<TItem>> _memberAccessors;

        internal TextTable(TextTableConfig<TItem> config)
        {
            _columns = config.Columns;
            _borders = config.BuildBorders();
            _header = new Header(_columns);
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


            // Write top border.
            _borders.Top.Write(writer, _columns, _borders);

            // Write header.
            _header.Write(writer, _borders);
            
            // Write Header and table separator.
            _borders.HeaderHorizontal.Write(writer, _columns, _borders);

            // Write table.
            if (rows.Any())
            {
                rows[0].WritePlanText(writer, _columns, _borders);
                for (var i = 1; i < rows.Count; i++)
                {
                    _borders.InsideHorizontal.Write(writer, _columns, _borders);
                    rows[i].WritePlanText(writer, _columns, _borders);
                }
            }

            // Write bottom border.
            _borders.Bottom.Write(writer, _columns, _borders);
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
