using System;
using System.Reflection;

namespace FluentTextTable
{
    public class TextTableBuilder
    {
        public static ITextTable<TItem> Build<TItem>()
        {
            var config = new TextTableConfig<TItem>();
            var propertyInfos = typeof(TItem).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var propertyInfo in propertyInfos)
            {
                config.AddColumn(propertyInfo);
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