using System;

namespace FluentTextTable
{
    public static class Build
    {
        public static ITextTable<TItem> TextTable<TItem>() => TextTable<TItem>(_ => { });

        public static ITextTable<TItem> TextTable<TItem>(Action<IPlainTextTableBuilder<TItem>> build)
        {
            var builder = new PlainTextTableBuilder<TItem>();
            build(builder);
            return builder.Build();
        }

        public static ITextTable<TItem> MarkdownTable<TItem>() => MarkdownTable<TItem>(_ => { });

        public static ITextTable<TItem> MarkdownTable<TItem>(Action<IMarkdownTableBuilder<TItem>> build)
        {
            var builder = new MarkdownTableBuilder<TItem>();
            build(builder);
            return builder.Build();
        }
    }
}