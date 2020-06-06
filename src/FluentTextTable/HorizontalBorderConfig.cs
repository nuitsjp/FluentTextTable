namespace FluentTextTable
{
    public class HorizontalBorderConfig : BorderConfigBase, IHorizontalBorderConfig
    {
        private char _leftEnd = '+';
        private char _line = '-';
        private char _intersection = '+';
        private char _rightEnd = '+';

        public IHorizontalBorderConfig LeftEndIs(char c)
        {
            _leftEnd = c;
            return this;
        }

        public IHorizontalBorderConfig LineIs(char c)
        {
            _line = c;
            return this;
        }

        public IHorizontalBorderConfig IntersectionIs(char c)
        {
            _intersection = c;
            return this;
        }

        public IHorizontalBorderConfig RightEndIs(char c)
        {
            _rightEnd = c;
            return this;
        }

        internal HorizontalBorder Build(
            VerticalBorder leftVerticalBorder,
            VerticalBorder insideVerticalBorder,
            VerticalBorder rightVerticalBorder)
        {
            return new HorizontalBorder(
                IsEnable,
                _line,
                _leftEnd,
                _intersection,
                _rightEnd,
                leftVerticalBorder,
                insideVerticalBorder,
                rightVerticalBorder);
        }
    }
}