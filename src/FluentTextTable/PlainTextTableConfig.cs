namespace FluentTextTable
{
    public class PlainTextTableConfig<TItem> : TextTableConfigBase<TItem>, IPlainTextTableConfig<TItem>
    {
        private readonly BordersConfig _borders = new BordersConfig();

        public IBordersConfig Borders => _borders;

        private IBorders BuildBorders() => _borders.Build();

        internal ITextTable<TItem> Build()
            => TextTable<TItem>.CreatePlainTextTable(BuildHeader(), BuildBorders(), Padding);
    }
}