using System;
using System.Linq;

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

        internal Borders Build()
        {
            ValidateAllWidthMatch(_top.LeftEndWidth, _headerHorizontal.LeftEndWidth, _insideHorizontal.LeftEndWidth, _bottom.LeftEndWidth, _left.LineWidth);
            ValidateAllWidthMatch(_top.IntersectionWidth, _headerHorizontal.IntersectionWidth, _insideHorizontal.IntersectionWidth, _bottom.IntersectionWidth, _insideVertical.LineWidth);
            ValidateAllWidthMatch(_top.RightEndWidth, _headerHorizontal.RightEndWidth, _insideHorizontal.RightEndWidth, _bottom.RightEndWidth, _right.LineWidth);
            
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

        private void ValidateAllWidthMatch(params int[] widths)
        {
            if(1 < widths.Distinct().Count())
                throw new InvalidOperationException("The widths of the vertical elements must match.");
        }

        public IHorizontalBorderConfig Top => _top;

        public IHorizontalBorderConfig HeaderHorizontal => _headerHorizontal;

        public IHorizontalBorderConfig InsideHorizontal => _insideHorizontal;

        public IHorizontalBorderConfig Bottom => _bottom;

        public IVerticalBorderConfig Left => _left;

        public IVerticalBorderConfig InsideVertical => _insideVertical;

        public IVerticalBorderConfig Right => _right;
        
        public IBordersConfig IsFullWidth()
        {
            Top.LeftEndIs("┌").LineIs("─").IntersectionIs("┬").RightEndIs("┐");
            HeaderHorizontal.LeftEndIs("├").LineIs("─").IntersectionIs("┼").RightEndIs("┤");
            InsideHorizontal.LeftEndIs("├").LineIs("─").IntersectionIs("┼").RightEndIs("┤");
            Bottom.LeftEndIs("└").LineIs("─").IntersectionIs("┴").RightEndIs("┘");
            Left.LineIs("│");
            InsideVertical.LineIs("│");
            Right.LineIs("│");
            return this;
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