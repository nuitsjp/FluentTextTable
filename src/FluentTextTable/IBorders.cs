namespace FluentTextTable
{
    public interface IBorders
    {
        IHorizontalBorder Top { get; }
        IHorizontalBorder HeaderHorizontal { get; }
        IHorizontalBorder InsideHorizontal { get; }
        IHorizontalBorder Bottom { get; }
        IVerticalBorder Left { get; }
        IVerticalBorder InsideVertical { get; }
        IVerticalBorder Right { get; }
        int HorizontalLineStyleLcd { get; }
    }
}