namespace FluentTextTable
{
    public class VerticalBorderConfig : BorderConfigBase, IVerticalBorderConfig
    {
        public char Line { get; private set; } = '|';

        public IVerticalBorderConfig LineIs(char c)
        {
            Line = c;
            return this;
        }
    }
}