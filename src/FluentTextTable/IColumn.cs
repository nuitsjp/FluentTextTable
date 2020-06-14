namespace FluentTextTable
{
    public interface IColumn
    {
        string Format { get; }
    }
    
    public interface IColumn<in TItem> : IColumn
    {
        object GetValue(TItem item);
        int HeaderWidth { get; }
        string Name { get; }
        HorizontalAlignment HorizontalAlignment { get; }
        VerticalAlignment VerticalAlignment { get; }
    }
}