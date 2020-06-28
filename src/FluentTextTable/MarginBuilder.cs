namespace FluentTextTable
{
    public class MarginBuilder<TItem> : CompositeTextTableBuilder<TItem>, IMarginBuilder<TItem>
    {
        private int _width;

        private readonly IMarginsBuilder<TItem> _marginsBuilder;

        public MarginBuilder(IMarginsBuilder<TItem> marginsBuilder, int width) : base(marginsBuilder)
        {
            _width = width;
            _marginsBuilder = marginsBuilder;
        }

        public IMarginsBuilder<TItem> As(int width)
        {
            _width = width;
            return _marginsBuilder;
        }

        internal IMargin Build() => new Margin(_width);
    }
}