namespace FluentTextTable
{
    public interface ITextTable<in TItem> : ITable<TItem>
    {
        Borders Borders { get; }
    }
}