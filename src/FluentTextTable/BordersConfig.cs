namespace FluentTextTable
{
    public class BordersConfig : BorderConfigBase, IBordersConfig
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
            return new Borders(
                _top.Build(),
                _headerHorizontal.Build(),
                _insideHorizontal.Build(),
                _bottom.Build(),
                _left.Build(),
                _insideVertical.Build(),
                _right.Build());
        }
    }
}