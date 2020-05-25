namespace FluentTextTable
{
    public interface ITextTableColumn
    {
        ITextTableColumn HeaderIs(string header);
        ITextTableColumn AlignHorizontalTo(HorizontalAlignment horizontalAlignment);
        ITextTableColumn AlignVerticalTo(VerticalAlignment verticalAlignment);
        ITextTableColumn FormatTo(string format);
    }
}