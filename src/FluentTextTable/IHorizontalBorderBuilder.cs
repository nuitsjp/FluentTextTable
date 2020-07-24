namespace FluentTextTable
{
    public interface IHorizontalBorderBuilder<TItem> : IBorderBuilder<TItem>
    {
        IHorizontalBorderBuilder<TItem> AllStylesAs(string s);
        IHorizontalBorderBuilder<TItem> LeftStyleAs(string s);
        IHorizontalBorderBuilder<TItem> LineStyleAs(string s);
        IHorizontalBorderBuilder<TItem> IntersectionStyleAs(string s);
        IHorizontalBorderBuilder<TItem> RightStyleAs(string s);
        IHorizontalBorderBuilder<TItem> AsDisable();
    }
}