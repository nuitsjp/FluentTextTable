namespace FluentTextTable
{
    public interface IColumn<in TItem>
    {
        string Name { get; }
        string Format { get; }
        int HeaderWidth { get; }
        HorizontalAlignment HorizontalAlignment { get; }
        VerticalAlignment VerticalAlignment { get; }
        object GetValue(TItem item);
    }
}