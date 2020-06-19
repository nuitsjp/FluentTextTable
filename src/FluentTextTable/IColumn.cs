namespace FluentTextTable
{
    public interface IColumn
    {
        string Name { get; }
        string Format { get; }
        int HeaderWidth { get; }
        HorizontalAlignment HorizontalAlignment { get; }
        VerticalAlignment VerticalAlignment { get; }
    }
    public interface IColumn<in TItem> : IColumn
    {
        object GetValue(TItem item);
    }
}