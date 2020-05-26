namespace FluentTextTable
{
    public interface IColumn
    {
        IColumn HeaderIs(string header);
        IColumn AlignHorizontalTo(HorizontalAlignment horizontalAlignment);
        IColumn AlignVerticalTo(VerticalAlignment verticalAlignment);
        IColumn FormatTo(string format);
    }
}