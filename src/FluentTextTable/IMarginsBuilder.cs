namespace FluentTextTable
{
    public interface IMarginsBuilder<TItem> : ITextTableBuilder<TItem>
    {
        IMarginsBuilder<TItem> As(int width);
        IMarginBuilder<TItem> Left { get; }
        IMarginBuilder<TItem> Right { get; }
    }
}