namespace FluentTextTable
{
    public interface IPaddingBuilder<TItem> : ITextTableBuilder<TItem>
    {
        IPaddingsBuilder<TItem> As(int width);
    }
}