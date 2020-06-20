namespace FluentTextTable
{
    public interface IPlainTextTableConfig<TItem> : ITextTableConfig<TItem>
    {
        IBordersConfig Borders { get; }
    }
}