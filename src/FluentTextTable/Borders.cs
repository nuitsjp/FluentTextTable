using System;

namespace FluentTextTable
{
    public class Borders
    {
        private static readonly VerticalBorder MarkdownVerticalBorder = new VerticalBorder(true, "|");

        private static readonly HorizontalBorder DisableHorizontalBorder =
            new HorizontalBorder(false, "-", "-", "|", "-", MarkdownVerticalBorder, MarkdownVerticalBorder,
                MarkdownVerticalBorder);

        internal static readonly Borders MarkdownTableBorders = new Borders(
            DisableHorizontalBorder,
            new MarkdownHeaderHorizontalBorder(),
            DisableHorizontalBorder,
            DisableHorizontalBorder,
            MarkdownVerticalBorder,
            MarkdownVerticalBorder,
            MarkdownVerticalBorder);

        internal Borders(
            HorizontalBorder top,
            HorizontalBorder headerHorizontal,
            HorizontalBorder insideHorizontal,
            HorizontalBorder bottom,
            VerticalBorder left,
            VerticalBorder insideVertical,
            VerticalBorder right)
        {
            Top = top;
            HeaderHorizontal = headerHorizontal;
            InsideHorizontal = insideHorizontal;
            Bottom = bottom;
            Left = left;
            InsideVertical = insideVertical;
            Right = right;
            HorizontalLineStyleLcd =
                MathEx.Lcm(top.LineStyle.GetWidth(), 
                    headerHorizontal.LineStyle.GetWidth(),
                    insideHorizontal.LineStyle.GetWidth(),
                    bottom.LineStyle.GetWidth());
        }

        public HorizontalBorder Top { get; }
        public HorizontalBorder HeaderHorizontal { get; }
        public HorizontalBorder InsideHorizontal { get; }
        public HorizontalBorder Bottom { get; }
        public VerticalBorder Left { get; }
        public VerticalBorder InsideVertical { get; }
        public VerticalBorder Right { get; }

        public int HorizontalLineStyleLcd { get; }
   }
}