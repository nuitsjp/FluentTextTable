namespace FluentTextTable
{
    public interface IPlainTextTableBuilder<TItem> : ITextTableBuilder<TItem>
    {
        IBordersBuilder<TItem> Borders { get; }
    }
}