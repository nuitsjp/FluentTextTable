namespace FluentTextTable
{
    public class PaddingBuilder<TItem> : CompositeTextTableBuilder<TItem>, IPaddingBuilder<TItem>
    {
        private readonly IPaddingsBuilder<TItem> _paddingsBuilder;
        private int _width;

        public PaddingBuilder(IPaddingsBuilder<TItem> paddingsBuilder, int width) : base(paddingsBuilder)
        {
            _paddingsBuilder = paddingsBuilder;
            _width = width;
        }

        public IPaddingsBuilder<TItem> As(int width)
        {
            _width = width;
            return _paddingsBuilder;
        }

        internal IPadding Build() => new Padding(_width);
    }
}