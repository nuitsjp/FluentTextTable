namespace FluentTextTable
{
    public interface IHorizontalBorderConfig : IBorderConfig
    {
        IHorizontalBorderConfig LeftEndIs(char c);
        IHorizontalBorderConfig LineIs(char c);
        IHorizontalBorderConfig IntersectionIs(char c);
        IHorizontalBorderConfig RightEndIs(char c);
    }
}