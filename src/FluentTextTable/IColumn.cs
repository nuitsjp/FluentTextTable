namespace FluentTextTable
{
    public interface IColumn
    {
        string Format { get; }
    }
    
    public interface IColumn<in TItem> : IColumn
    {
        object GetValue(TItem item);
    }
}