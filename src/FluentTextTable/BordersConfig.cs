namespace FluentTextTable
{
    public class BordersConfig : IBordersConfig
    {
        private readonly HorizontalBorderConfig _top = new HorizontalBorderConfig();
        private readonly HorizontalBorderConfig _headerHorizontal = new HorizontalBorderConfig();
        private readonly HorizontalBorderConfig _insideHorizontal = new HorizontalBorderConfig();
        private readonly HorizontalBorderConfig _bottom = new HorizontalBorderConfig();
        private readonly VerticalBorderConfig _left = new VerticalBorderConfig();
        private readonly VerticalBorderConfig _insideVertical = new VerticalBorderConfig();
        private readonly VerticalBorderConfig _right = new VerticalBorderConfig();

        public IHorizontalBorderConfig Top => _top;

        public IHorizontalBorderConfig HeaderHorizontal => _headerHorizontal;

        public IHorizontalBorderConfig InsideHorizontal => _insideHorizontal;

        public IHorizontalBorderConfig Bottom => _bottom;

        public IVerticalBorderConfig Left => _left;

        public IVerticalBorderConfig InsideVertical => _insideVertical;

        public IVerticalBorderConfig Right => _right;

        internal Borders Build()
        {
            var left = _left.Build();
            var insideVertical = _insideVertical.Build();
            var right = _right.Build();
            return new Borders(
                _top.Build(left, insideVertical, right),
                _headerHorizontal.Build(left, insideVertical, right),
                _insideHorizontal.Build(left, insideVertical, right),
                _bottom.Build(left, insideVertical, right),
                left,
                insideVertical,
                right);
        }

        public void Disable()
        {
            _top.Disable();
            _headerHorizontal.Disable();
            _insideHorizontal.Disable();
            _bottom.Disable();
            _left.Disable();
            _insideVertical.Disable();
            _right.Disable();
        }
    }
}