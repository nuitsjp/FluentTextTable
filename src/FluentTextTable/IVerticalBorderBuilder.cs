namespace FluentTextTable
{
    public interface IVerticalBorderBuilder<TItem> : IBorderBuilder<TItem>
    {
        IVerticalBorderBuilder<TItem> LeftStyleAs(string s);
    }
}