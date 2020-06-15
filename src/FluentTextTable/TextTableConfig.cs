namespace FluentTextTable
{
    public class TextTableConfig<TItem> : TableConfig<TItem>, ITextTableConfig<TItem>
    {
        private readonly BordersConfig _borders = new BordersConfig();

        public IBordersConfig Borders => _borders;

        internal Borders BuildBorders() => _borders.Build();

        internal TextTable<TItem> Build()
        {
            return new TextTable<TItem>(Padding, BuildColumns(), BuildBorders());
        }
    }
}