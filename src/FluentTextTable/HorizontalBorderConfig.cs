namespace FluentTextTable
{
    public class HorizontalBorderConfig : BorderConfigBase, IHorizontalBorderConfig
    {
        public char LeftEnd { get; private set; } = '+';
        public char Line { get; private set; } = '-';
        public char Intersection { get; private set; } = '+';
        public char RightEnd { get; private set; } = '+';

        public IHorizontalBorderConfig LeftEndIs(char c)
        {
            LeftEnd = c;
            return this;
        }

        public IHorizontalBorderConfig LineIs(char c)
        {
            Line = c;
            return this;
        }

        public IHorizontalBorderConfig IntersectionIs(char c)
        {
            Intersection = c;
            return this;
        }

        public IHorizontalBorderConfig RightEndIs(char c)
        {
            RightEnd = c;
            return this;
        }
    }
}