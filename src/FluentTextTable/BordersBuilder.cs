using System;
using System.Linq;

namespace FluentTextTable
{
    public class BordersBuilder<TItem> : CompositeTextTableBuilder<TItem>, IBordersBuilder<TItem>
    {
        private readonly IHorizontalBorderBuilder<TItem> _horizontals;
        private readonly HorizontalBorderBuilder<TItem> _top;
        private readonly HorizontalBorderBuilder<TItem> _headerHorizontal;
        private readonly HorizontalBorderBuilder<TItem> _insideHorizontal;
        private readonly HorizontalBorderBuilder<TItem> _bottom;
        private readonly IVerticalBorderBuilder<TItem> _verticals;
        private readonly VerticalBorderBuilder<TItem>  _left;
        private readonly VerticalBorderBuilder<TItem>  _insideVertical;
        private readonly VerticalBorderBuilder<TItem>  _right;

        public BordersBuilder(ITextTableBuilder<TItem> textTableBuilder) : base(textTableBuilder)
        {
            _top = new HorizontalBorderBuilder<TItem>(TextTableBuilder);
            _headerHorizontal = new HorizontalBorderBuilder<TItem>(TextTableBuilder);
            _insideHorizontal = new HorizontalBorderBuilder<TItem>(TextTableBuilder);
            _bottom = new HorizontalBorderBuilder<TItem>(TextTableBuilder);
            _horizontals = new HorizontalsBorderBuilder<TItem>(TextTableBuilder, _top, _headerHorizontal, _insideHorizontal, _bottom);
            _left = new VerticalBorderBuilder<TItem>(TextTableBuilder);
            _insideVertical = new VerticalBorderBuilder<TItem>(TextTableBuilder);
            _right = new VerticalBorderBuilder<TItem>(TextTableBuilder);
            _verticals = new VerticalsBorderBuilder<TItem>(TextTableBuilder, _left, _insideVertical, _right);
        }

        public IHorizontalBorderBuilder<TItem> Horizontals => _horizontals;

        public IHorizontalBorderBuilder<TItem> Top => _top;

        public IHorizontalBorderBuilder<TItem> HeaderHorizontal => _headerHorizontal;

        public IHorizontalBorderBuilder<TItem> InsideHorizontal => _insideHorizontal;

        public IHorizontalBorderBuilder<TItem> Bottom => _bottom;

        public IVerticalBorderBuilder<TItem> Verticals => _verticals;

        public IVerticalBorderBuilder<TItem>  Left => _left;

        public IVerticalBorderBuilder<TItem>  InsideVertical => _insideVertical;

        public IVerticalBorderBuilder<TItem>  Right => _right;
        
        internal IBorders Build()
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

        public IBordersBuilder<TItem> AsFullWidthStyle()
        {
            Top.LeftStyleAs("┌").LineStyleAs("─").IntersectionStyleAs("┬").RightStyleAs("┐");
            HeaderHorizontal.LeftStyleAs("┝").LineStyleAs("━").IntersectionStyleAs("┿").RightStyleAs("┥");
            InsideHorizontal.LeftStyleAs("├").LineStyleAs("─").IntersectionStyleAs("┼").RightStyleAs("┤");
            Bottom.LeftStyleAs("└").LineStyleAs("─").IntersectionStyleAs("┴").RightStyleAs("┘");
            Left.LineStyleAs("│");
            InsideVertical.LineStyleAs("│");
            Right.LineStyleAs("│");
            return this;
        }

        public IBordersBuilder<TItem> AsDisable()
        {
            _top.AsDisable();
            _headerHorizontal.AsDisable();
            _insideHorizontal.AsDisable();
            _bottom.AsDisable();
            _left.AsDisable();
            _insideVertical.AsDisable();
            _right.AsDisable();
            return this;
        }
    }
}