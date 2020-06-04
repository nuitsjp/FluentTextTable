namespace FluentTextTable
{
    internal interface ITextTable<TItem>
    {
        internal int GetColumnWidth(Column<TItem> column);
    }
}