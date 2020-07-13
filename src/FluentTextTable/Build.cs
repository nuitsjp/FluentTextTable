using System;

namespace FluentTextTable
{
    /// <summary>
    /// Build a Plain Text Table or Markdown Table.
    /// </summary>
    public static class Build
    {
        /// <summary>
        /// Build a Plain Text Table.
        /// </summary>
        /// <typeparam name="TItem">The class of a single-row object in Table.</typeparam>
        /// <returns>Plain Text Table.</returns>
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