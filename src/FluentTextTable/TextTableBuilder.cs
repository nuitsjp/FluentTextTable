using System;
using System.Linq;
using System.Reflection;

namespace FluentTextTable
{
    public class TextTableBuilder
    {
        public static ITextTable<TItem> Build<TItem>()
        {
            var config = new TextTableConfig<TItem>();
            var memberInfos = 
                typeof(TItem).GetMembers(BindingFlags.Public | BindingFlags.Instance)
                    .Where(x => x.MemberType == MemberTypes.Field || x.MemberType == MemberTypes.Property);
            foreach (var memberInfo in memberInfos)
            {
                config.AddColumn(memberInfo);
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