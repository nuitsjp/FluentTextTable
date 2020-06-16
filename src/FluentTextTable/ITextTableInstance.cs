namespace FluentTextTable
{
    public interface ITextTableInstance<TItem> : ITableInstance<TItem>
    {
        Borders Borders { get; }
    }
}