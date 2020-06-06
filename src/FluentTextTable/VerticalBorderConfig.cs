namespace FluentTextTable
{
    public class VerticalBorderConfig : BorderConfigBase, IVerticalBorderConfig
    {
        private char _line  = '|';

        public IVerticalBorderConfig LineIs(char c)
        {
            _line = c;
            return this;
        }

        internal VerticalBorder Build()
        {
            return new VerticalBorder(IsEnable, _line);
        }
    }
}