using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using EastAsianWidthDotNet;

namespace FluentTextTable
{
    public class MarkdownTableWriter<TItem> : ITextTableWriter<TItem>
    {
        private readonly List<Column<TItem>> _columns;
        private readonly Headers<TItem> _headers;
        private readonly Borders _borders;

        private MarkdownTableWriter(TextTableConfig<TItem> config, List<Column<TItem>> columns)
        {
            _columns = columns;
            _borders = config.BuildBorders();
            _headers = new Headers<TItem>(_columns, _borders);
        }

        public string ToString(IEnumerable<TItem> items)
        {
            var writer = new StringWriter();
            Write(writer, items);
            return writer.ToString();
        }

        public void Write(TextWriter writer, IEnumerable<TItem> items)
        {
            var body = new Body<TItem>(_columns, _borders, items);
            var table = new TextTable<TItem>(_columns, _headers, body, _borders);
            Write(writer, table, body);
        }

        private void Write(TextWriter textWriter, ITextTable<TItem> table, Body<TItem> body)
        {
            var headerSeparator = new StringBuilder();
            textWriter.Write("|");
            headerSeparator.Append("|");
            foreach (var column in _columns)
            {
                textWriter.Write(" ");

                textWriter.Write(column.Name);
                textWriter.Write(new string(' ', table.GetColumnWidth(column) - column.Name.GetWidth() - 2)); // TODO Fix -> column.Header.GetWidth() - 2

                switch (column.HorizontalAlignment)
                {
                    case HorizontalAlignment.Default:
                        headerSeparator.Append(new string('-', table.GetColumnWidth(column)));
                        break;
                    case HorizontalAlignment.Left:
                        headerSeparator.Append(':');
                        headerSeparator.Append(new string('-', table.GetColumnWidth(column) - 1));
                        break;
                    case HorizontalAlignment.Center:
                        headerSeparator.Append(':');
                        headerSeparator.Append(new string('-', table.GetColumnWidth(column) - 2));
                        headerSeparator.Append(':');
                        break;
                    case HorizontalAlignment.Right:
                        headerSeparator.Append(new string('-', table.GetColumnWidth(column) - 1));
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

            // Write table.
            body.WriteMarkdown(textWriter, (TextTable<TItem>)table);
        }

        public static ITextTableWriter<TItem> Build()
        {
            var config = new TextTableConfig<TItem>();
            AddColumns(config);
            return new MarkdownTableWriter<TItem>(config, config.FixColumnSpecs());
        }

        public static ITextTableWriter<TItem> Build(Action<ITextTableConfig<TItem>> configure)
        {
            var config = new TextTableConfig<TItem>();
            configure(config);
            if (config.AutoGenerateColumns)
            {
                AddColumns(config);
            }
            return new MarkdownTableWriter<TItem>(config, config.FixColumnSpecs());
        }
        
        private static void AddColumns(TextTableConfig<TItem> config)
        {
            var memberInfos =
                typeof(TItem).GetMembers(BindingFlags.Public | BindingFlags.Instance)
                    .Where(x => x.MemberType == MemberTypes.Field || x.MemberType == MemberTypes.Property);
            var members = new List<(int index, MemberInfo memberInfo, ColumnFormatAttribute columnFormat)>();
            foreach (var memberInfo in memberInfos)
            {
                var columnFormat = memberInfo.GetCustomAttribute<ColumnFormatAttribute>();
                if (columnFormat is null)
                {
                    members.Add((0, memberInfo, null));
                }

                if (columnFormat != null)
                {
                    members.Add((columnFormat.Index, memberInfo, columnFormat));
                }
            }

            foreach (var member in members.OrderBy(x => x.index))
            {
                var column = config.AddColumn(member.memberInfo);
                if (member.columnFormat != null)
                {
                    if (member.columnFormat.Header != null) column.NameIs(member.columnFormat.Header);
                    column
                        .AlignHorizontalTo(member.columnFormat.HorizontalAlignment)
                        .AlignVerticalTo(member.columnFormat.VerticalAlignment)
                        .FormatTo(member.columnFormat.Format);
                }
            }
        }
    }
}
