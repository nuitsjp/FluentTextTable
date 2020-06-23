namespace FluentTextTable
{
    public class HorizontalBorderConfig : IHorizontalBorderConfig
    {
        private bool _isEnable  = true;

        private string _leftEnd = "+";
        private string _line = "-";
        private string _intersection = "+";
        private string _rightEnd = "+";

        internal int LeftEndWidth => _leftEnd.GetWidth();
        internal int IntersectionWidth => _intersection.GetWidth();
        internal int RightEndWidth => _rightEnd.GetWidth();

        public void Disable()
        {
            _isEnable = false;
        }

        public IHorizontalBorderConfig LeftEndIs(string s)
        {
            _leftEnd = s;
            return this;
        }

        public IHorizontalBorderConfig LineIs(string s)
        {
            _line = s;
            return this;
        }

        public IHorizontalBorderConfig IntersectionIs(string s)
        {
            _intersection = s;
            return this;
        }

        public IHorizontalBorderConfig RightEndIs(string s)
        {
            _rightEnd = s;
            return this;
        }

        internal IHorizontalBorder Build(
            IVerticalBorder leftVerticalBorder,
            IVerticalBorder insideVerticalBorder,
            IVerticalBorder rightVerticalBorder)
        {
            return new HorizontalBorder(
                _isEnable,
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