namespace FluentTextTable
{
    public interface IBordersBuilder<TItem> : IBorderBuilder<TItem>
    {
        IHorizontalBorderBuilder<TItem> Horizontals { get; }
        IHorizontalBorderBuilder<TItem> Top { get; }
        IHorizontalBorderBuilder<TItem> HeaderHorizontal { get; }
        IHorizontalBorderBuilder<TItem> InsideHorizontal { get; }
        IHorizontalBorderBuilder<TItem> Bottom { get; }
        IVerticalBorderBuilder<TItem> Verticals { get; }
        IVerticalBorderBuilder<TItem> Left { get; }
        IVerticalBorderBuilder<TItem> InsideVertical { get; }
        IVerticalBorderBuilder<TItem> Right { get; }
        IBordersBuilder<TItem> AllStylesAs(string s);
        IBordersBuilder<TItem> AsFullWidthStyle();
        IBordersBuilder<TItem> AsDisable();
    }
}