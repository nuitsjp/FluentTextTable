namespace FluentTextTable
{
    public class HorizontalBorderBuilder<TItem> : CompositeTextTableBuilder<TItem>, IHorizontalBorderBuilder<TItem>
    {
        private bool _isEnable  = true;

        private string _leftEnd = "+";
        private string _line = "-";
        private string _intersection = "+";
        private string _rightEnd = "+";

        public HorizontalBorderBuilder(ITextTableBuilder<TItem> textTableBuilder) : base(textTableBuilder)
        {
        }

        internal int LeftEndWidth => _leftEnd.GetWidth();
        internal int IntersectionWidth => _intersection.GetWidth();
        internal int RightEndWidth => _rightEnd.GetWidth();

        public void AsDisable()
        {
            _isEnable = false;
        }

        public IHorizontalBorderBuilder<TItem> LeftStyleAs(string s)
        {
            _leftEnd = s;
            return this;
        }

        public IHorizontalBorderBuilder<TItem> LineStyleAs(string s)
        {
            _line = s;
            return this;
        }

        public IHorizontalBorderBuilder<TItem> IntersectionStyleAs(string s)
        {
            _intersection = s;
            return this;
        }

        public IHorizontalBorderBuilder<TItem> RightStyleAs(string s)
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