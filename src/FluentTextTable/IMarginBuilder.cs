namespace FluentTextTable
{
    public interface IMarginBuilder<TItem> : ITextTableBuilder<TItem>
    {
        IMarginsBuilder<TItem> As(int margin);
    }
}