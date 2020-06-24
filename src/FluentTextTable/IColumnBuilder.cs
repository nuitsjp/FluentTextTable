namespace FluentTextTable
{
    public interface IColumnBuilder<TItem> : ITextTableBuilder<TItem>
    {
        IColumnBuilder<TItem> NameAs(string name);
        IColumnBuilder<TItem> HorizontalAlignmentAs(HorizontalAlignment horizontalAlignment);
        IColumnBuilder<TItem> VerticalAlignmentAs(VerticalAlignment verticalAlignment);
        IColumnBuilder<TItem> FormatAs(string format);
    }
}