namespace FluentTextTable
{
    public class MarginsBuilder<TItem> : CompositeTextTableBuilder<TItem>, IMarginsBuilder<TItem>
    {
        private readonly MarginBuilder<TItem> _left;
        private readonly MarginBuilder<TItem> _right;

        public MarginsBuilder(ITextTableBuilder<TItem> textTableBuilder) : base(textTableBuilder)
        {
            _left = new MarginBuilder<TItem>(this, 1);
            _right = new MarginBuilder<TItem>(this, 0);
        }

        public IMarginsBuilder<TItem> As(int margin)
        {
            _left.As(margin);
            _right.As(margin);
            return this;
        }

        public IMarginBuilder<TItem> Left => _left;
        public IMarginBuilder<TItem> Right => _right;

        internal IMargins Build() => new Margins(_left.Build(), _right.Build());
    }
}