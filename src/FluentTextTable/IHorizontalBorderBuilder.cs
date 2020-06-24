namespace FluentTextTable
{
    public interface IHorizontalBorderBuilder<TItem> : IBorderBuilder<TItem>
    {
        IHorizontalBorderBuilder<TItem> LeftStyleAs(string s);
        IHorizontalBorderBuilder<TItem> LineStyleAs(string s);
        IHorizontalBorderBuilder<TItem> IntersectionStyleAs(string s);
        IHorizontalBorderBuilder<TItem> RightStyleAs(string s);
    }
}