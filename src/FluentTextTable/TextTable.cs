using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using FluentTextTable.PlanText;

namespace FluentTextTable
{
    public class TextTable<TItem> : ITextTable<TItem>
    {
        public IReadOnlyList<Column<TItem>> Columns { get; }

        private TextTable(List<Column<TItem>> columns, Borders borders)
        {
            Columns = columns;
            Borders = borders;
        }

        public Borders Borders { get; }

        public string ToString(IEnumerable<TItem> items)
        {
            var writer = new StringWriter();
            Write(writer, items);
            return writer.ToString();
        }

        public void Write(TextWriter writer, IEnumerable<TItem> items)
        {
            var rows = new List<Row<TItem>>();
            foreach (var item in items)
            {
                rows.Add(new Row<TItem>(Columns, item));
            }
            var rowSet = new RowSet<TItem>(Columns, rows);
            
            Borders.Top.Write(writer, this, rowSet);
            this.WriteHeader(writer, rowSet);
            Borders.HeaderHorizontal.Write(writer, this, rowSet);
            rowSet.Write(writer, this);
            Borders.Bottom.Write(writer, this, rowSet);
        }
        
        public static TextTable<TItem> Build()
        {
            var config = new TextTableConfig<TItem>();
            AddColumns(config);
            return new TextTable<TItem>(config.FixColumnSpecs(), config.BuildBorders());
        }

        public static TextTable<TItem> Build(Action<ITextTableConfig<TItem>> configure)
        {
            var config = new TextTableConfig<TItem>();
            configure(config);
            if (config.AutoGenerateColumns)
            {
                AddColumns(config);
            }
            return new TextTable<TItem>(config.FixColumnSpecs(), config.BuildBorders());
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