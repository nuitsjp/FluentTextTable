namespace FluentTextTable
{
    public interface IBordersConfig : IBorderConfig
    {
        IHorizontalBorderConfig Top { get; }
        IHorizontalBorderConfig HeaderHorizontal { get; }
        IHorizontalBorderConfig InsideHorizontal { get; }
        IHorizontalBorderConfig Bottom { get; }
        IVerticalBorderConfig Left { get; }
        IVerticalBorderConfig InsideVertical { get; }
        IVerticalBorderConfig Right { get; }
        IBordersConfig IsFullWidth();
    }
}