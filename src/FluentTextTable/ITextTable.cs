namespace FluentTextTable
{
    public interface ITextTable<TItem> : ITable<TItem>
    {
        Borders Borders { get; }
    }
}