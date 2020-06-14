using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using FluentTextTable.Markdown;

namespace FluentTextTable
{
    public class MarkdownTable<TItem> : ITable<TItem>
    {
        public IReadOnlyList<Column<TItem>> Columns { get; }

        private MarkdownTable(List<Column<TItem>> columns)
        {
            Columns = columns;
        }

        public string ToString(IEnumerable<TItem> items)
        {
            var writer = new StringWriter();
            Write(writer, items);
            return writer.ToString();
        }

        public void Write(TextWriter textWriter, IEnumerable<TItem> items)
        {
            var rows = new List<IRow<TItem>>();
            foreach (var item in items)
            {
                rows.Add(new Row<TItem>(Columns, item));
            }
            var rowSet = new RowSet<TItem>(Columns, rows);

            this.WriteHeader(textWriter, rowSet);
            rowSet.Write(textWriter, this);
        }

        public static ITable<TItem> Build()
        {
            var config = new TableConfig<TItem>();
            AddColumns(config);
            return new MarkdownTable<TItem>(config.FixColumnSpecs());
        }

        public static ITable<TItem> Build(Action<ITableConfig<TItem>> configure)
        {
            var config = new TableConfig<TItem>();
            configure(config);
            if (config.AutoGenerateColumns)
            {
                AddColumns(config);
            }
            return new MarkdownTable<TItem>(config.FixColumnSpecs());
        }
        
        private static void AddColumns(TableConfig<TItem> config)
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
