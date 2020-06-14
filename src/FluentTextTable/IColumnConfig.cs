namespace FluentTextTable
{
    public interface IColumnConfig
    {
        IColumnConfig HasName(string name);
        IColumnConfig AlignHorizontal(HorizontalAlignment horizontalAlignment);
        IColumnConfig AlignVertical(VerticalAlignment verticalAlignment);
        IColumnConfig HasFormat(string format);
    }
}