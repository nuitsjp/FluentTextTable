namespace FluentTextTable
{
    public class MarkdownTableConfig<TItem> : TextTableConfigBase<TItem>, IMarkdownTableConfig<TItem>
    {

        public ITextTable<TItem> Build()
            => TextTable<TItem>.CreateMarkdownTable(BuildHeader(), Borders.MarkdownTableBorders, Padding);
    }
}