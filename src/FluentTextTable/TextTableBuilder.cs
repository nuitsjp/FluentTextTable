using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FluentTextTable
{
    public static class TextTableBuilder
    {
        public static ITextTable<TItem> Build<TItem>()
        {
            var config = new TextTableConfig<TItem>();
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
            return new TextTable<TItem>(config);
        }

        public static ITextTable<TItem> Build<TItem>(Action<ITextTableConfig<TItem>> configure)
        {
            var config = new TextTableConfig<TItem>();
            configure(config);
            return new TextTable<TItem>(config);
        }
    }
}