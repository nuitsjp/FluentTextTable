namespace FluentTextTable
{
    public interface IPaddingsBuilder<TItem> : ITextTableBuilder<TItem>
    {
        IPaddingsBuilder<TItem> As(int width);
        IPaddingBuilder<TItem> Left { get; }
        IPaddingBuilder<TItem> Right { get; }

    }
}