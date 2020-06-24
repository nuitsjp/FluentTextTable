namespace FluentTextTable
{
    public interface IBorderBuilder<TItem> : ITextTableBuilder<TItem>
    {
        void AsDisable();
    }
}