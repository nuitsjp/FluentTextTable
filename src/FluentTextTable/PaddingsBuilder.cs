namespace FluentTextTable
{
    public class PaddingsBuilder<TItem> : CompositeTextTableBuilder<TItem>, IPaddingsBuilder<TItem>
    {
        private readonly PaddingBuilder<TItem> _left;
        private readonly PaddingBuilder<TItem> _right;

        public PaddingsBuilder(ITextTableBuilder<TItem> textTableBuilder) : base(textTableBuilder)
        {
            _left = new PaddingBuilder<TItem>(this, Padding.DefaultWidth);
            _right = new PaddingBuilder<TItem>(this, Padding.DefaultWidth);
        }

        public IPaddingsBuilder<TItem> As(int width)
        {
            _left.As(width);
            _right.As(width);
            return this;
        }

        public IPaddingBuilder<TItem> Left => _left;

        public IPaddingBuilder<TItem> Right => _right;

        internal IPaddings Build() => new Paddings(_left.Build(), _right.Build());
    }
}