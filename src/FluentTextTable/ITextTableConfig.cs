namespace FluentTextTable
{
    public interface ITextTableConfig<TItem> : ITableConfig<TItem>
    {
        IBordersConfig Borders { get; }
    }
}