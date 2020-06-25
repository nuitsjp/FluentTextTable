namespace FluentTextTable
{
    public interface IMarginsBuilder<TItem> : ITextTableBuilder<TItem>
    {
        IMarginsBuilder<TItem> As(int margin);
        IMarginBuilder<TItem> Left { get; }
        IMarginBuilder<TItem> Right { get; }
    }
}