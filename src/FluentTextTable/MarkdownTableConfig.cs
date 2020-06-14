namespace FluentTextTable
{
    public class MarkdownTableConfig<TItem> : TextTableConfig<TItem>
    {
        public MarkdownTable<TItem> Build()
        {
            return new MarkdownTable<TItem>(BuildColumns());
        }
    }
}