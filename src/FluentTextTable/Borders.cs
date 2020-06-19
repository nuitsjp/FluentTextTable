namespace FluentTextTable
{
    public class Borders
    {
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
        }

        public HorizontalBorder Top { get; }
        public HorizontalBorder HeaderHorizontal { get; }
        public HorizontalBorder InsideHorizontal { get; }
        public HorizontalBorder Bottom { get; }
        public VerticalBorder Left { get; }
        public VerticalBorder InsideVertical { get; }
        public VerticalBorder Right { get; }
    }
}