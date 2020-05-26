using System;

namespace FluentTextTable
{
    public class TextTableBuilder
    {
        public static ITextTable<TItem> Build<TItem>(Action<ITextTableConfig<TItem>> configure)
        {
            var config = new TextTableConfig<TItem>();
            configure(config);
            return new TextTable<TItem>(config);
        }

    }
}