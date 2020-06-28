namespace FluentTextTable
{
    public class MarginsBuilder<TItem> : CompositeTextTableBuilder<TItem>, IMarginsBuilder<TItem>
    {
        private readonly MarginBuilder<TItem> _left;
        private readonly MarginBuilder<TItem> _right;

        public MarginsBuilder(ITextTableBuilder<TItem> textTableBuilder) : base(textTableBuilder)
        {
            _left = new MarginBuilder<TItem>(this, Margin.LeftDefaultWidth);
            _right = new MarginBuilder<TItem>(this, Margin.RightDefaultWidth);
        }

        public IMarginsBuilder<TItem> As(int width)
        {
            _left.As(width);
            _right.As(width);
            return this;
        }

        public IMarginBuilder<TItem> Left => _left;
        public IMarginBuilder<TItem> Right => _right;

        internal IMargins Build() => new Margins(_left.Build(), _right.Build());
    }
}