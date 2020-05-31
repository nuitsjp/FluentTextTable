namespace FluentTextTable
{
    public interface IColumnConfig
    {
        IColumnConfig HeaderIs(string header);
        IColumnConfig AlignHorizontalTo(HorizontalAlignment horizontalAlignment);
        IColumnConfig AlignVerticalTo(VerticalAlignment verticalAlignment);
        IColumnConfig FormatTo(string format);
    }
}