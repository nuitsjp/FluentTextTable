using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FluentTextTable
{
    public class TextTable<TItem> : ITextTable<TItem>
    {
        private readonly List<Column<TItem>> _columns;
        private readonly Headers<TItem> _headers;
        private readonly Borders _borders;

        internal TextTable(TextTableConfig<TItem> config, List<Column<TItem>> columns)
        {
            _columns = columns;
            _borders = config.BuildBorders();
            _headers = new Headers<TItem>(_columns);
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
            var body = new Body<TItem>(_columns, DataSource);
            new TextTableWriter<TItem>(_columns, _headers, body, _borders).WritePlanText(writer);
        }

        public string ToMarkdown()
        {
            var writer = new StringWriter();
            WriteMarkdown(writer);
            return writer.ToString();
        }

        public void WriteMarkdown(TextWriter writer)
        {
            var body = new Body<TItem>(_columns, DataSource);
            new TextTableWriter<TItem>(_columns, _headers, body, _borders).WriteMarkdown(writer);
        }
        
        public static ITextTable<TItem> Build()
        {
            var config = new TextTableConfig<TItem>();
            AddColumns(config);
            return new TextTable<TItem>(config, config.FixColumnSpecs());
        }

        public static ITextTable<TItem> Build(Action<ITextTableConfig<TItem>> configure)
        {
            var config = new TextTableConfig<TItem>();
            configure(config);
            if (config.AutoGenerateColumns)
            {
                AddColumns(config);
            }
            return new TextTable<TItem>(config, config.FixColumnSpecs());
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
                    if (member.columnFormat.Header != null) column.HeaderIs(member.columnFormat.Header);
                    column
                        .AlignHorizontalTo(member.columnFormat.HorizontalAlignment)
                        .AlignVerticalTo(member.columnFormat.VerticalAlignment)
                        .FormatTo(member.columnFormat.Format);
                }
            }
        }
    }
}
