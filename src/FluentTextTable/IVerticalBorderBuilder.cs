namespace FluentTextTable
{
    public interface IVerticalBorderBuilder<TItem> : IBorderBuilder<TItem>
    {
        IVerticalBorderBuilder<TItem> LineStyleAs(string s);
        IVerticalBorderBuilder<TItem> AsDisable();
    }
}