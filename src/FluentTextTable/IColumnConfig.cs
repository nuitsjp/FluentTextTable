namespace FluentTextTable
{
    public interface IColumnConfig
    {
        IColumnConfig NameIs(string name);
        IColumnConfig AlignHorizontalTo(HorizontalAlignment horizontalAlignment);
        IColumnConfig AlignVerticalTo(VerticalAlignment verticalAlignment);
        IColumnConfig FormatTo(string format);
    }
}