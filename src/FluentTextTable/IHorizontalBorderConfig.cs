namespace FluentTextTable
{
    public interface IHorizontalBorderConfig : IBorderConfig
    {
        IHorizontalBorderConfig LeftEndIs(string s);
        IHorizontalBorderConfig LineIs(string s);
        IHorizontalBorderConfig IntersectionIs(string s);
        IHorizontalBorderConfig RightEndIs(string s);
    }
}