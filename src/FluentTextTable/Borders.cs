using System;

namespace FluentTextTable
{
    public class Borders : IBorders
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
            IHorizontalBorder top,
            IHorizontalBorder headerHorizontal,
            IHorizontalBorder insideHorizontal,
            IHorizontalBorder bottom,
            IVerticalBorder left,
            IVerticalBorder insideVertical,
            IVerticalBorder right)
        {
            Top = top;
            HeaderHorizontal = headerHorizontal;
            InsideHorizontal = insideHorizontal;
            Bottom = bottom;
            Left = left;
            InsideVertical = insideVertical;
            Right = right;
            HorizontalLineStyleLcd =
                MathEx.Lcm(top.LineStyleWidth, 
                    headerHorizontal.LineStyleWidth,
                    insideHorizontal.LineStyleWidth,
                    bottom.LineStyleWidth);
        }

        public IHorizontalBorder Top { get; }
        public IHorizontalBorder HeaderHorizontal { get; }
        public IHorizontalBorder InsideHorizontal { get; }
        public IHorizontalBorder Bottom { get; }
        public IVerticalBorder Left { get; }
        public IVerticalBorder InsideVertical { get; }
        public IVerticalBorder Right { get; }

        public int HorizontalLineStyleLcd { get; }
   }
}