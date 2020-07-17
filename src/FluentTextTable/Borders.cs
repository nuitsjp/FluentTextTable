using System;

namespace FluentTextTable
{
    /// <summary>
    /// All the borders of the table.
    /// </summary>
    public class Borders : IBorders
    {
        /// <summary>
        /// The vertical border of the markdown table.
        /// </summary>
        private static readonly VerticalBorder MarkdownVerticalBorder = new VerticalBorder(true, "|");

        /// <summary>
        /// The horizontal hidden border of the markdown table.
        /// </summary>
        private static readonly HorizontalBorder DisableHorizontalBorder =
            new HorizontalBorder(false, "-", "-", "|", "-", MarkdownVerticalBorder, MarkdownVerticalBorder,
                MarkdownVerticalBorder);

        /// <summary>
        /// All the borders of the markdown table.
        /// </summary>
        internal static readonly Borders MarkdownTableBorders = new Borders(
            DisableHorizontalBorder,
            new MarkdownHeaderHorizontalBorder(),
            DisableHorizontalBorder,
            DisableHorizontalBorder,
            MarkdownVerticalBorder,
            MarkdownVerticalBorder,
            MarkdownVerticalBorder);

        /// <summary>
        /// Initializes a new instance of the Borders class.
        /// </summary>
        /// <param name="top"></param>
        /// <param name="headerHorizontal"></param>
        /// <param name="insideHorizontal"></param>
        /// <param name="bottom"></param>
        /// <param name="left"></param>
        /// <param name="insideVertical"></param>
        /// <param name="right"></param>
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