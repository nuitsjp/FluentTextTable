namespace FluentTextTable
{
    public class MarginBuilder<TItem> : CompositeTextTableBuilder<TItem>, IMarginBuilder<TItem>
    {
        private int _margin;

        private IMarginsBuilder<TItem> _marginsBuilder;

        public MarginBuilder(IMarginsBuilder<TItem> marginsBuilder, int defaultValue) : base(marginsBuilder)
        {
            _margin = defaultValue;
            _marginsBuilder = marginsBuilder;
        }

        public IMarginBuilder<TItem> Left => _marginsBuilder.Left;
        public IMarginBuilder<TItem> Right => _marginsBuilder.Right;

        public IMarginsBuilder<TItem> As(int margin)
        {
            _margin = margin;
            return _marginsBuilder;
        }

        internal IMargin Build() => new Margin(_margin);
    }
}